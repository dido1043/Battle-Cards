namespace Program.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    public class UsersController: Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            
            return this.View("/Users/Login");
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View("/Users/Register");
        }
        public HttpResponse DoLogin(HttpRequest request)
        {
            //TODO:read data
            //TODO:check user
            //TODO:log user
            return this.Redirect("/");
        }
    }
}
