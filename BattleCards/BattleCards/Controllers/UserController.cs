using SIS.HTTP;
using SIS.MvcFramework;
using System.Text;


namespace Program.Controllers
{
    public class UserController: Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            
            return this.View("View/Users/Login.html");
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View("View/Users/Register.html");
        }
    }
}
