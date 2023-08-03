namespace Program.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class CardsController :Controller
    {
        public HttpResponse All(HttpRequest request)
        {

            return this.View("View/Cards/All.html");
        }

        public HttpResponse Add(HttpRequest request)
        {

            return this.View("View/Cards/Add.html");
        }
        public HttpResponse Collection(HttpRequest request)
        {

            return this.View("");
        }
    }
}
