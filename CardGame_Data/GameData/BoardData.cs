using System;

namespace CardGame_Data.GameData
{
    [Serializable]
    public class BoardData
    {
        public BoardSideData LeftBoardSide { get; set; }
        public BoardSideData RightBoardSide { get; set; }
    }
}
