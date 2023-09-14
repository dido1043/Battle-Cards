using BattleCards.ViewModels;
namespace BattleCards.Services
{
    public interface ICardsService
    {
        int AddCard(string name, string image, string keyword, int attack, int health);

        void AddCardToUserCollection(string userId, int cardId);

        void RemoveCardFromUserCollection(string userId, int cardId);

        IEnumerable<CardsViewModel> GetAllCards();

        IEnumerable<CardsViewModel> GetAllCardsByUser(string id);

    }
}
