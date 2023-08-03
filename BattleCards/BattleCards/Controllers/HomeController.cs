namespace Program.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Text;

    public class HomeController : Controller
    {
        public HttpResponse HomePage(HttpRequest request)
        {
            return this.View("View/Home/home.html");
        }
        
        public HttpResponse About(HttpRequest request)
        {
            var responseHtml = "<h1>About...</h1>";

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);

            response.Headers.Add(new Header("Server:", "SIS Server 1.0"));

            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
            { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });

            return response;
        }
    }
}
