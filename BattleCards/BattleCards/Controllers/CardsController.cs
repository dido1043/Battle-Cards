namespace Program.Controllers
{
    using Program.Data;
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
            var dbContext = new ApplicationDbContext();

            dbContext.Cards.Add(new Card() 
            { 
                Name = this.Request.FormData["name"],
                ImageUrl = this.Request.FormData["image"],
                Keyword = this.Request.FormData["keyword"],
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"])
            });
            dbContext.SaveChanges();

            return this.Redirect("/");
        }
        public HttpResponse Collection()
        {

            return this.View();
        }
    }
}
