using CardGame_Test.Cards;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Test.BoardTable
{
    public class BoardSite
    {
        public IReadOnlyList<Field> Fields { get; }
        public IList<LandCard> LandCards { get; }

        public BoardSite()
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
             f.Index.X == field.Index.X - 1 && f.Index.Y == field.Index.Y - 1 ||
             f.Index.X == field.Index.X - 1 && f.Index.Y == field.Index.Y + 1 ||
             f.Index.X == field.Index.X + 1 && f.Index.Y == field.Index.Y + 1 ||
             f.Index.X == field.Index.X + 1 && f.Index.Y == field.Index.Y - 1 ||
             f.Index.X == field.Index.X + 1 && f.Index.Y == field.Index.Y ||
             f.Index.X == field.Index.X - 1 && f.Index.Y == field.Index.Y ||
             f.Index.X == field.Index.X && f.Index.Y == field.Index.Y + 1 ||
             f.Index.X == field.Index.X && f.Index.Y == field.Index.Y - 1
             );
        }

    }
}
