namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.ViewModels;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class CardsController : Controller
    {
        public HttpResponse Collection()
        {
            var db = new ApplicationDbContext();
       
            var cardsViewModel = db.Cards.Select(x => new CardsViewModel
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Keyword = x.Keyword,
                Attack = x.Attack,
                Health = x.Health
            }).ToList();
            return this.View(new AllCardsViewModel() { Cards = cardsViewModel});
            
        }

        public HttpResponse Add()
        {

            return this.View();
        }

        [HttpPost("/cards/add")]
        public HttpResponse DoAdd()
        {
            var dbContext = new ApplicationDbContext();

            if (this.Request.FormData["name"].Length < 5 || this.Request.FormData["name"].Length > 15)
            {
                
                return this.Error(HttpErrorText.InvalidTextLength);
            }

            if (int.Parse(this.Request.FormData["attack"]) < 0)
            {
                return this.Error(HttpErrorText.InvalidAttack);   
            }

            if (int.Parse(this.Request.FormData["health"]) < 0)
            {
                return this.Error(HttpErrorText.InvalidHealth);
            }
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
       
    }
}
