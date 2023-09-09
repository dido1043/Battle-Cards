using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SIS.MvcFramework.ViewEngine
{
    public class SISViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel, string user)
        {
            var csharpCode = GenerateCSharpFromTemplate(templateCode, viewModel);
            IView executableObject = GenerateExecutableObject(csharpCode, viewModel);
            var html = executableObject.ExecuteTemplate(viewModel, user);
            return html;
        }

        private string GenerateCSharpFromTemplate(string templateCode, object viewModel)
        {
            string typeOfModel = "object";
            if (viewModel != null)
            {
                if (viewModel.GetType().IsGenericType)
                {
                    var modelName = viewModel.GetType().FullName;
                    var genericArguments = viewModel.GetType().GenericTypeArguments;
                    typeOfModel = modelName.Substring(0, modelName.IndexOf('`'))
                        + "<" + string.Join(",", genericArguments.Select(x => x.FullName)) + ">";
                }
                else
                {
                    typeOfModel = viewModel.GetType().FullName;
                }
            }
            string csharpCode = @"
                using System;
                using System.Text;
                using System.Linq;
                using System.Collections.Generic;
                using SIS.MvcFramework.ViewEngine;
                  namespace ViewNamespace
                 {
                    public class ViewClass : IView
                    {
                        public string ExecuteTemplate(object viewModel,string user)
                        {
                           var User = user;
                           var Model = viewModel as " + typeOfModel + @";
                           var html = new StringBuilder();"
                            + GetMethodBody(templateCode) +
                          @"return html.ToString();
                        }
                    }
                 }";
            return csharpCode;
        }

        private string GetMethodBody(string templateCode)
        {
            Regex regex = new Regex(@"[^\<\""\s&]+", RegexOptions.Compiled);
            List<string> supportedOperators = new List<string>()
            {"for","foreach", "while", "if", "else if", "else" };
            StringBuilder csharpCode = new StringBuilder();
            StringReader reader = new StringReader(templateCode);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Replace(System.Environment.NewLine, "");

                if (supportedOperators.Any(x => line.TrimStart().StartsWith("@" + x)))
                {
                    var location = line.IndexOf("@");
                    line = line.Remove(location, 1);
                    csharpCode.AppendLine(line);
                }
                else if (line.TrimStart().StartsWith("{") ||
                    line.TrimStart().StartsWith("}"))
                {
                    csharpCode.AppendLine(line);
                }
                else
                {
                    csharpCode.Append($"html.AppendLine(@\"");
                    while (line.Contains("@"))
                    {
                        var atSignLocation = line.IndexOf("@");
                        var htmlBeforeAtSign = line.Substring(0, atSignLocation);
                        csharpCode.Append(htmlBeforeAtSign.Replace("\"", "\"\"") + "\" + ");
                        var lineAfterAtSign = line.Substring(atSignLocation + 1);
                        var code = regex.Match(lineAfterAtSign).Value;
                        csharpCode.Append(code + " + @\"");
                        line = lineAfterAtSign.Substring(code.Length);

                    }
                    //csharpCode.AppendLine($"html.AppendLine(@\"{line.Replace("\"", "\"\"")}\");");

                    csharpCode.Append(line.Replace("\"", "\"\"") + "\");");
                }

            }
            return csharpCode.ToString();
        }
        //Code for generating .dll files
        private IView GenerateExecutableObject(string code, object model)
        {
            var compilation = CSharpCompilation.Create("ViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
            if (model != null)
            {
                compilation = compilation.AddReferences(MetadataReference.CreateFromFile(model.GetType().Assembly.Location));
            }

            var libraries = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();
            foreach (var library in libraries)
            {
                compilation = compilation.AddReferences(
                    MetadataReference.CreateFromFile(Assembly.Load(library).Location));
            }

            compilation = compilation.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(code));

            using var memoryStream = new MemoryStream();
            memoryStream.Read(new byte[1024]);
            var compilationResult = compilation.Emit(memoryStream);
            if (!compilationResult.Success)
            {
                return new ErrorView(
                    compilationResult.Diagnostics
                    .Where(x => x.Severity == DiagnosticSeverity.Error)
                    .Select(x => x.GetMessage()), code);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            var assemblyByteArray = memoryStream.ToArray();
            var assembly = Assembly.Load(assemblyByteArray);
            var type = assembly.GetType("ViewNamespace.ViewClass");
            var instance = Activator.CreateInstance(type) as IView;
            return instance;
        }

    }
}
