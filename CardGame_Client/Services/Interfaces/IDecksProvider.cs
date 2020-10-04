using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardGame_Client.Services.Interfaces
{
    public interface IDecksProvider
    {
        Task<IEnumerable<string>> GetDecks();
    }
}