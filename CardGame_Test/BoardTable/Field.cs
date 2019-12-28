using CardGame_Test.Units;

namespace CardGame_Test.BoardTable
{
    public class Field
    {
        public Coord Index { get; }
        public Unit Unit { get; set; }

        public Field(int x, int y)
        {
            Index = new Coord(x, y);
        }
    }
}
