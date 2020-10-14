using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.GameEvents.Interfaces;

namespace CardGame_Game.BoardTable
{
    public class Board : IBoard
    {
        public IBoardSide LeftBoardSite { get; }
        public IBoardSide RightBoardSite { get; }

        public Board(IGameEventsContainer gameEventsContainer)
        {
            LeftBoardSite = new BoardSide(gameEventsContainer);
            RightBoardSite = new BoardSide(gameEventsContainer);

            LeftBoardSite.EnemyBoardSide = RightBoardSite;
            RightBoardSite.EnemyBoardSide = LeftBoardSite;
        }
    }
}
