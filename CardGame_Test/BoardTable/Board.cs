using System;
using System.Collections;

namespace CardGame_Test.BoardTable
{
    public class Board
    {
        public BoardSite LeftBoardSite { get; set; }
        public BoardSite RightBoardSite { get; set; }

        public Board()
        {
            LeftBoardSite = new BoardSite();
            RightBoardSite = new BoardSite();
        }
    }
}
