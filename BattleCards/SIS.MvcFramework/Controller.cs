using SIS.HTTP;
using System.Text;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View(string path)
        {
           var layout = System.IO.File.ReadAllText("View/Shared/_Layout.html");
            
            
            var viewContent = File.ReadAllText(path);

            var responseHtml = layout.Replace("@RenderBody()", viewContent) ;

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);


            response.Headers.Add(new Header("Server:", "SIS Server 1.0"));

            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
            { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });

            return response;
        }
    }
}