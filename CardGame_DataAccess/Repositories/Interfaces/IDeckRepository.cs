using CardGame_DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardGame_DataAccess.Repositories.Interfaces
{
    public interface IDeckRepository
    {
        Task CreateDeck(Deck deck);
        Task<IEnumerable<Deck>> GetDecks();
        Task<Deck> GetDeck(int id);
        Task<Deck> GetDeck(string deckName);
        Task RemoveDeck(string deckName);
    }
}