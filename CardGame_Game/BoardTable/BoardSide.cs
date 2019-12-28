using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.BoardTable
{
    public class BoardSide : IBoardSide
    {
        public IReadOnlyList<Field> Fields { get; }

        public IList<ILandCard> LandCards { get; } = new List<ILandCard>();

        public event EventHandler<GameEventArgs> TurnStarting;
        public event EventHandler<GameEventArgs> TurnStarted;

        public BoardSide()
        {
            Fields = new List<Field>
            {
                new Field(0,0),
                new Field(0,1),
                new Field(0,2),
                new Field(0,3),
                new Field(0,4),
                new Field(1,0),
                new Field(1,1),
                new Field(1,2),
                new Field(1,3),
                new Field(1,4),
                new Field(2,0),
                new Field(2,1),
                new Field(2,2),
                new Field(2,3),
                new Field(2,4),
            };
        }

        public IEnumerable<Field> GetNeighbourFields(Field field)
        {
            return Fields.Where(f =>
             f.X == field.X - 1 && f.Y == field.Y - 1 ||
             f.X == field.X - 1 && f.Y == field.Y + 1 ||
             f.X == field.X + 1 && f.Y == field.Y + 1 ||
             f.X == field.X + 1 && f.Y == field.Y - 1 ||
             f.X == field.X + 1 && f.Y == field.Y ||
             f.X == field.X - 1 && f.Y == field.Y ||
             f.X == field.X && f.Y == field.Y + 1 ||
             f.X == field.X && f.Y == field.Y - 1
             );
        }

        public void AddLandCard(ILandCard card)
        {
            LandCards.Add(card);
        }

        public void StartTurn(IGame game)
        {
            TurnStarting?.Invoke(this, new GameEventArgs { Game = game });
            TurnStarted?.Invoke(this, new GameEventArgs { Game = game });
        }
    }
}
