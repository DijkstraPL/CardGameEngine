using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using System;
using System.Collections.Generic;

namespace CardGame_Game.Players.Interfaces
{
    public interface IPlayer
    {
        string Name { get; }
        CardColor PlayerColor { get;  }
        int Energy { get; }
        bool CardTaken { get; }

        IBoardSide BoardSide { get; set; }

        Stack<GameCard> LandDeck { get; }
        Stack<GameCard> Deck { get; }
        IList<GameCard> Hand { get; }

        bool IsLandCardPlayed { get; set; }

        void PrepareForGame();
        void RegisterTriggers(IGame game);

        void EndTurn();

        void GetCardFromDeck();
        void GetCardFromLandDeck();

        void SetStartingHand();
        void IncreaseEnergy(CardColor cardColor, int value);
    }
}
