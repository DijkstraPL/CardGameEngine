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
    public interface IPlayer : IHealthy
    {
        string Name { get; }
        CardColor PlayerColor { get; }
        int Energy { get; }
        //int HitPoints { get; set; }
        //int MaxHitPoints { get; set; }
        bool CardTaken { get; }

        IBoardSide BoardSide { get; set; }

        bool IsLoser { get; }

        Stack<GameCard> LandDeck { get; }
        Stack<GameCard> Deck { get; }
        Stack<GameCard> Graveyard { get; }
        IList<GameCard> Hand { get; }
        bool IsLandCardPlayed { get; set; }

        void PrepareForGame();
        void RegisterTriggers(IGame game);

        void EndTurn();

        bool GetCardFromDeck();
        bool GetCardFromLandDeck();

        void SetStartingHand();
        void IncreaseEnergy(CardColor cardColor, int value);
        void AddToGraveyard(GameCard card);
        void OnCardMove();
        bool CanMove();
    }
}
