using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.MvcFramework.ViewEngine
{
    public class ErrorView : IView
    {
        private readonly IEnumerable<string> _errors;
        private readonly string _csharpCode;
        public ErrorView(IEnumerable<string> errors, string csharpCode)
        {
            this._errors=errors;
            this._csharpCode=csharpCode;
        }
        public string ExecuteTemplate(object viewModel, string user)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<h1>View compile {this._errors.Count()} errors:</h1> <ul>");
            foreach (var error in this._errors)
            {
                sb.AppendLine($"<li>{error}</li>");
            }
            sb.AppendLine($"</ul> <pre>{this._csharpCode}</pre>");
            return sb.ToString();
        }
    }
}
