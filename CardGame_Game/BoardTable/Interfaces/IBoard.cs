using CardGame_Game.Cards.Interfaces;

namespace CardGame_Game.BoardTable.Interfaces
{
    public interface IBoard
    {
        IBoardSide LeftBoardSite { get; }
        IBoardSide RightBoardSite { get; }
    }
}
