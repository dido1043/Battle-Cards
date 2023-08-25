namespace Program.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class CardsController :Controller
    {
        public HttpResponse All(HttpRequest request)
        {

            return this.View("Cards/All");
        }

        public HttpResponse Add(HttpRequest request)
        {

            return this.View("/Cards/Add");
        }
        public HttpResponse Collection(HttpRequest request)
        {

            return this.View("/Cards/All");
        }
    }
}
