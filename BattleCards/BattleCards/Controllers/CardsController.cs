namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.ViewModels;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class CardsController : Controller
    {
        private ApplicationDbContext dbContext;

        public CardsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/users/login");
            }
            //var db = new ApplicationDbContext();

            var cardsViewModel = this.dbContext.Cards.Select(x => new CardsViewModel
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Keyword = x.Keyword,
                Attack = x.Attack,
                Health = x.Health
            }).ToList();
            return this.View(new AllCardsViewModel() { Cards = cardsViewModel });

        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/users/login");
            }
            return this.View();
        }

        [HttpPost("/cards/add")]
        public HttpResponse DoAdd(string name, string image, string keyword, int attack, int health)
        {
            //var dbContext = new ApplicationDbContext();

            if (name.Length < 5 || name.Length > 15)
            {

                return this.Error(HttpErrorText.InvalidTextLength);
            }

            if (attack < 0)
            {
                return this.Error(HttpErrorText.InvalidAttack);
            }

            if (health < 0)
            {
                return this.Error(HttpErrorText.InvalidHealth);
            }
            this.dbContext.Cards.Add(new Card()
            {
                Name = name,
                ImageUrl = image,
                Keyword = keyword,
                Attack = attack,
                Health = health
            });
            this.dbContext.SaveChanges();

            return this.Redirect("/");
        }

    }
}