using CardGame_DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardGame_DataAccess.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task CreateCard(Card card);
        Task<IEnumerable<Card>> GetCards();
        Task<Card> GetCard(int id);
        Task<Card> GetCard(string name);
    }
}