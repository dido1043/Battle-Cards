using SIS.HTTP;
using SIS.MvcFramework;


namespace Program.Controllers
{
    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            return this.File("wwwroot/favicon.ico", "image/vnd.microsoft.icon");
        }
        public HttpResponse MainCss(HttpRequest request)
        {

            return this.File("wwwroot/css/main.css","text/css");
        }
    }
}
