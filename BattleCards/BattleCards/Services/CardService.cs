using BattleCards.Data;
using BattleCards.ViewModels;

namespace BattleCards.Services
{
    public class CardService : ICardsService
    {
        private ApplicationDbContext dbContetxt;
        private IUserSevices userService;
        public CardService(ApplicationDbContext dbContext, IUserSevices userService)
        {
            this.dbContetxt = dbContext;
            this.userService = userService;
        }
        public int AddCard(string name, string image, string keyword, int attack, int health)
        {
            var card = new Card
            {
                Name = name,
                ImageUrl = image,
                Keyword = keyword,
                Health = health,
                Attack = attack,
            };
            this.dbContetxt.Cards.Add(card);
            this.dbContetxt.SaveChanges();
            return card.Id;

        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            if (!this.dbContetxt.UserCards.Any(x => x.UserId == userId && x.CardId == cardId))
            {  
                return; 
            }
            this.dbContetxt.UserCards.Add(new UserCard 
            { 
                CardId = cardId,
                UserId = userId
            });
            this.dbContetxt.SaveChanges();
        }

        public IEnumerable<CardsViewModel> GetAllCards()
        {
            var allCards = this.dbContetxt.Cards.Select(x => new CardsViewModel{
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Keyword = x.Keyword,
                Health = x.Health,
                Attack = x.Attack,
            }).ToList();
            return allCards;
        }

        public IEnumerable<CardsViewModel> GetAllCardsByUser(string id)
        {
            var userCards = this.dbContetxt.UserCards
                .Where(x => x.UserId == id)
                .Select(x => new CardsViewModel 
                { 
                    Name =x.Card.Name,
                    ImageUrl = x.Card.ImageUrl,
                    Keyword = x.Card.Keyword,   
                    Health = x.Card.Health,
                    Attack = x.Card.Attack
                }).ToList();
            return userCards;
        }

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            var userCard = this.dbContetxt.UserCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);
            if (userCard == null)
            {
                return;
            }

            this.dbContetxt.UserCards.Remove(userCard);
        }
    }
}
