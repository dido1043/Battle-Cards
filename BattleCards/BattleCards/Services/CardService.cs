using BattleCards.Data;


namespace BattleCards.Services
{
    public class CardService : ICardsService
    {
        private ApplicationDbContext dbContetxt;

        public CardService(ApplicationDbContext dbContext)
        {
                this.dbContetxt = dbContext;
        }
        public void AddCard()
        {
            throw new NotImplementedException();
        }
    }
}
