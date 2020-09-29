using System;
using System.Collections.Generic;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;

namespace CardGame_Game.BoardTable.Interfaces
{
    public interface IBoardSide
    {
        IList<GameLandCard> LandCards { get; }

        IEnumerable<Field> GetNeighbourFields(Field field);
        void AddLandCard(GameLandCard card);
        void StartTurn(IGame game);
    }
}
