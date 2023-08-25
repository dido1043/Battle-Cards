namespace Program.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Text;

    public class HomeController : Controller
    {
        public HttpResponse Home(HttpRequest request)
        {
            return this.View("/Home/Home");
        }
        
 
    }
}
