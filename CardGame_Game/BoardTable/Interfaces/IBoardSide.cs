using System;
using System.Collections.Generic;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;

namespace CardGame_Game.BoardTable.Interfaces
{
    public interface IBoardSide
    {
        IList<ILandCard> LandCards { get; }
        event EventHandler<GameEventArgs> TurnStarting;
        event EventHandler<GameEventArgs> TurnStarted;

        IEnumerable<Field> GetNeighbourFields(Field field);
        void AddLandCard(ILandCard card);
        void StartTurn(IGame game);
    }
}
