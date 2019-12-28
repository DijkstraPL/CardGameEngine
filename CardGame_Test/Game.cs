using CardGame_Test.BoardTable;
using System;
using System.Linq;

namespace CardGame_Test
{
    public class Game
    {
        public int TurnCounter { get; private set; }

        public Player LeftPlayer { get; } 
        public Player RightPlayer { get; }

        public Player CurrentPlayer { get; private set; }

        public Board Board { get; }

        private Random _random = new Random();

        public Game(Player playerOne, Player playerTwo)
        {
            Board = new Board();

            LeftPlayer = playerOne;
            RightPlayer = playerTwo;

            CurrentPlayer = new Player[] { LeftPlayer, RightPlayer }.OrderBy(p => _random.Next()).First();
        }

        public void NextTurn()
        {
            if (CurrentPlayer == LeftPlayer)
                CurrentPlayer = RightPlayer;
            else
                CurrentPlayer = LeftPlayer;

            CurrentPlayer.NextTurn();
          //  CurrentPlayer.RefreshMana(CurrentPlayer.BoardSite.LandCards.Sum(lc => lc.GetMana()));
        }

        public void Start()
        {
            TurnCounter = 1;


        }
    }
}
