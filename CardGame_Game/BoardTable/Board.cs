using CardGame_Game.BoardTable.Interfaces;

namespace CardGame_Game.BoardTable
{
    public class Board : IBoard
    {
        public IBoardSide LeftBoardSite { get; }
        public IBoardSide RightBoardSite { get; }

        public Board()
        {
            LeftBoardSite = new BoardSide();
            RightBoardSite = new BoardSide();
        }
    }
}
