namespace Program.Controllers
{
    using Program.ViewModels;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class CardsController : Controller
    {
        public HttpResponse All()
        {

            return this.View();
        }

        public HttpResponse Add()
        {

            return this.View();
        }
        [HttpPost("/cards/add")]
        public HttpResponse DoAdd()
        {
            var viewModel = new DoAddViewModel
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"])
            };
            return this.View(viewModel);
        }
        public HttpResponse Collection()
        {

            return this.View();
        }
    }
}
