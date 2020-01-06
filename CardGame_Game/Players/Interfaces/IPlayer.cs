using CardGame_Data.Entities.Enums;
using CardGame_Game.BoardTable.Interfaces;
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
        CardColor PlayerColor { get; }
        IDictionary<CardColor, int> Energy { get; }
        bool CardTaken { get; }

        IBoardSide BoardSide { get; set; }

        Stack<ILandCard> LandDeck { get; }
        Stack<ICard> Deck { get; }

        bool IsLandCardPlayed { get; set; }

        event EventHandler<GameEventArgs> LandCardPlayed;

        void ShuffleLandDeck();
        void ShuffleDeck();

        void EndTurn();

        void GetCardFromDeck();
        void GetCardFromLandDeck();
        void IncreaseEnergy(CardColor cardColor, int amount);
        IEnumerable<ICard> GetHand();
        void PlayLandCard(IGame game, ILandCard card);
    }
}
