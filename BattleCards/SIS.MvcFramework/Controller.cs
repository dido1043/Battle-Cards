namespace SIS.MvcFramework
{
    using SIS.HTTP;
    using SIS.MvcFramework.ViewEngine;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class Controller
    {
        private SISViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new SISViewEngine();
        }
        public HttpRequest Request { get; set; }
        public HttpResponse View(object viewModel = null, [CallerMemberName] string path = null)
        {
            var layout = System.IO.File.ReadAllText("View/Shared/_Layout.cshtml");


            //var viewContent = System.IO.File.ReadAllText(path);
            var viewContent = System.IO.File.ReadAllText(
                "View/" +
                this.GetType().Name.Replace("Controller", string.Empty) +
                "/" + path + ".cshtml");
            viewContent = this.viewEngine.GetHtml(viewContent, viewModel);
            var responseHtml = layout.Replace("@RenderBody()", viewContent);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);


            response.Headers.Add(new Header("Server:", "SIS Server 1.0"));

            

            return response;
        }

        public HttpResponse File(string filePath, string type)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(type, fileBytes);
            return response;
        }

        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatus.Found);
            response.Headers.Add(new Header("Location", url));
            return response;
        }
        public HttpResponse Error(string errorText)
        {
            var layout = System.IO.File.ReadAllText("View/Shared/_Layout.cshtml");

            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">\r\n  {errorText}\r\n</div>";
            var responseHtml = layout.Replace("@RenderBody()", viewContent);
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
    }
}
