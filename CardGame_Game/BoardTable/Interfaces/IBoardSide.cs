using System;
using System.Collections.Generic;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;

namespace CardGame_Game.BoardTable.Interfaces
{
    public interface IBoardSide
    {
        IList<GameLandCard> LandCards { get; }
        IReadOnlyList<Field> Fields { get; }
        IBoardSide EnemyBoardSide { get; set; }

        IEnumerable<Field> GetNeighbourFields(Field field);
        void AddLandCard(GameLandCard card);
        void StartTurn(IGame game);
        void Move(IPlayer player, Field start, Field target);
        void FinishTurn(IGame game, IPlayer player);
        void Kill(GameUnitCard gameUnitCard);
    }
}
