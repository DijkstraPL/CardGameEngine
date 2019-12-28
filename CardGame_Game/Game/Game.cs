using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;
using System.Linq;

namespace CardGame_Game.Game
{

    public class Game : IGame
    {
        public int TurnCounter { get; private set; }
        public IPlayer FirstPlayer { get; }
        public IPlayer SecondPlayer { get; }
        public IBoard Board { get; }
        public IPlayer CurrentPlayer { get; private set; }
        public IPlayer NextPlayer { get; private set; }

        public event EventHandler<GameEventArgs> TurnFinished;

        public Game(IPlayer firstPlayer, IPlayer secondPlayer, IBoard board)
        {
            FirstPlayer = firstPlayer ?? throw new ArgumentNullException(nameof(firstPlayer));
            SecondPlayer = secondPlayer ?? throw new ArgumentNullException(nameof(secondPlayer));
            Board = board ?? throw new ArgumentNullException(nameof(board));
            FirstPlayer.BoardSide = Board.LeftBoardSite;
            SecondPlayer.BoardSide = Board.RightBoardSite;
        }

        public void StartGame()
        {
            FirstPlayer.ShuffleDeck();
            FirstPlayer.ShuffleLandDeck();

            SecondPlayer.ShuffleDeck();
            SecondPlayer.ShuffleLandDeck();

            if (FlipCoin())
            {
                CurrentPlayer = FirstPlayer;
                NextPlayer = SecondPlayer;
            }
            else
            {
                CurrentPlayer = SecondPlayer;
                NextPlayer = FirstPlayer;
            }
        }

        private bool FlipCoin()
        {
            var random = new Random();
            return random.Next(0, 2) == 0;
        }

        public void NextTurn()
        {
            TurnFinished?.Invoke(this, new GameEventArgs { Game = this });
            CurrentPlayer.EndTurn();
            TurnCounter++;

            if (FirstPlayer == CurrentPlayer)
            {
                NextPlayer = FirstPlayer;
                CurrentPlayer = SecondPlayer;
            }
            else
            {
                NextPlayer = SecondPlayer;
                CurrentPlayer = FirstPlayer;
            }

            CurrentPlayer.BoardSide.StartTurn(this);
        }

        public void GetCardFromDeck()
        {
            if (!CurrentPlayer.CardTaken)
                CurrentPlayer.GetCardFromDeck();
        }

        public void GetCardFromLandDeck()
        {
            if (!CurrentPlayer.CardTaken)
                CurrentPlayer.GetCardFromLandDeck();
        }

        public void PlayLandCard(ILandCard card)
        {
            if (CurrentPlayer.Energy >= card.Cost &&
                CurrentPlayer.GetHand().Any(h => h == card))
                CurrentPlayer.PlayLandCard(this, card);
        }
    }
}
