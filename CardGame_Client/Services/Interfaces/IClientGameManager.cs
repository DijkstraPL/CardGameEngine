using CardGame_Data.GameData;
using System;
using System.Threading.Tasks;

namespace CardGame_Client.Services.Interfaces
{
    public interface IClientGameManager
    {
        public GameData GameData { get; }

        event EventHandler<GameData> GameStarted; 
        event EventHandler<GameData> CardTaken;
        event EventHandler<GameData> TurnStarted;

        Task SetReady(string playerName, string deckName);
        Task FinishTurn();
        Task DrawLandCard();
        Task DrawCard();
    }
}