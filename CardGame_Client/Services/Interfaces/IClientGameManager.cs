using System.Threading.Tasks;

namespace CardGame_Client.Services.Interfaces
{
    public interface IClientGameManager
    {
        Task SetReady(string playerName, string deckName);
    }
}