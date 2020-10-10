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
        event EventHandler<GameData> CardPlayed;

        Task SetReady(string playerName, string deckName);
        Task FinishTurn();
        Task DrawLandCard();
        Task DrawCard();
        Task PlayCard(CardData cardData, SelectionTargetData selectionTargetData);
        Task SetAttackTarget(CardData attackSource, CardData attackTarget);
    }
}