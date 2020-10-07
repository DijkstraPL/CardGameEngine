using CardGame_Game.BoardTable;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;

namespace CardGame_Game.Game.Interfaces
{
    public interface IGame
    {
        int TurnCounter { get; }

        IPlayer CurrentPlayer { get; }
        IPlayer NextPlayer { get;  }
        IBoard Board { get; }

        IGameEventsContainer GameEventsContainer { get; }

        void StartGame();
        void FinishTurn();
        bool GetCardFromDeck();
        bool GetCardFromLandDeck();
        void PlayCard(GameCard card, InvocationData invocationData);
    }
}
