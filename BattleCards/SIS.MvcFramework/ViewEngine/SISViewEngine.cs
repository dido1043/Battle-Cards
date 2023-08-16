using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework.ViewEngine
{
    public class SISViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            var csharpCode = GenerateCSharpFromTemplate(templateCode);
            IView executableObject = GenerateExecutableObject(csharpCode, viewModel);
            var html = executableObject.ExecuteTemplate(viewModel);
            return html;
        }

        private string GenerateCSharpFromTemplate(string templateCode)
        {

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
                        public string ExecuteTemplate(object viewModel)
                        {
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
            StringBuilder csharpCode = new StringBuilder();
            StringReader reader = new StringReader(templateCode);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                
                csharpCode.AppendLine($"html.AppendLine(@\"{line.Replace("\"","\"\"")}\");");
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
                   .Select(x => x.GetMessage()),code);
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
