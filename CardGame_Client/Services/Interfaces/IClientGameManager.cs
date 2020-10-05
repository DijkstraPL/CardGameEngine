using CardGame_Data.GameData;
using System;
using System.Threading.Tasks;

namespace CardGame_Client.Services.Interfaces
{
    public interface IClientGameManager
    {
        event EventHandler<GameData> GameStarted;
        Task SetReady(string playerName, string deckName);
    }
}