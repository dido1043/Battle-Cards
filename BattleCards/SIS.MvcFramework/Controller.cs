namespace SIS.MvcFramework
{
    using SIS.HTTP;
    using SIS.MvcFramework.ViewEngine;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class Controller
    {
        private const string UserIdSessionName = "UserId";
        private SISViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new SISViewEngine();
        }
        public HttpRequest Request { get; set; }
        protected HttpResponse View(object viewModel = null, [CallerMemberName] string path = null)
        {
            var viewContent = System.IO.File.ReadAllText(
                "View/" +
                this.GetType().Name.Replace("Controller", string.Empty) +
                "/" + path + ".cshtml");
            viewContent = this.viewEngine.GetHtml(viewContent, viewModel, this.GetUserId());
            var responseHtml = PutViewInLayout(viewContent, viewModel);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);


            response.Headers.Add(new Header("Server:", "SIS Server 1.0"));

            

            return response;
        }

        private string PutViewInLayout(string viewContent , object viewModel)
        {
            var layout = System.IO.File.ReadAllText("View/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "VIEW__GOES__HERE");
            layout = this.viewEngine.GetHtml(layout, viewModel, this.GetUserId());
            var responseHtml = layout.Replace("VIEW__GOES__HERE", viewContent);
            return responseHtml;
        }

        protected HttpResponse File(string filePath, string type)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(type, fileBytes);
            return response;
        }
        protected HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatus.Found);
            response.Headers.Add(new Header("Location", url));
            return response;
        }
        protected HttpResponse Error(string errorText)
        {
            var layout = System.IO.File.ReadAllText("View/Shared/_Layout.cshtml");

            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">\r\n  {errorText}\r\n</div>";
            var responseHtml = layout.Replace("@RenderBody()", viewContent);
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            
            var response = new HttpResponse("text/html", responseBodyBytes,HttpStatus.ServerError);
            return response;
        }

        protected void SignIn(string userId)
        {
            this.Request.SessionData[UserIdSessionName] = userId;
        }

        protected void SignOut()
        {
            this.Request.SessionData[UserIdSessionName] = null;
        }

        protected bool IsUserSignedIn() =>
            this.Request.SessionData.ContainsKey(UserIdSessionName) &&
            this.Request.SessionData[UserIdSessionName] != null;

        protected string GetUserId() =>
            this.Request.SessionData.ContainsKey(UserIdSessionName) ?
            this.Request.SessionData[UserIdSessionName] : null;
    }
}
