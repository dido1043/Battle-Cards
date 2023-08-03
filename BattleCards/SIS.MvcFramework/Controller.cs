namespace SIS.MvcFramework
{
    using SIS.HTTP;
    using System.Text;

    public abstract class Controller
    {
        public HttpResponse View(string path)
        {
            var layout = System.IO.File.ReadAllText("View/Shared/_Layout.html");


            var viewContent = System.IO.File.ReadAllText(path);

            var responseHtml = layout.Replace("@RenderBody()", viewContent);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);


            response.Headers.Add(new Header("Server:", "SIS Server 1.0"));

            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
            { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });

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

    }
}
