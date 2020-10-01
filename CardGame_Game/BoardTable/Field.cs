using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;

namespace CardGame_Game.BoardTable
{
    public class Field : IField
    {
        public int X { get; }
        public int Y { get; }

        public GameUnitCard Card { get; set; }

        public Field(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
