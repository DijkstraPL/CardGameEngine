using CardGame_Data.Entities.Enums;
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

        public event EventHandler<GameEventArgs> GameStarting;
        public event EventHandler<GameEventArgs> GameStarted;
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
            GameStarting?.Invoke(this, new GameEventArgs { Game = this });

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
            
            GameStarted?.Invoke(this, new GameEventArgs { Game = this });
        }

        private bool FlipCoin()
        {
            var random = new Random();
            return random.Next(0, 2) == 0;
        }

        public void FinishTurn()
        {
            TurnFinished?.Invoke(this, new GameEventArgs { Game = this });
            NextTurn();
        }

        public void NextTurn()
        {
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
            if (CurrentPlayer.Energy[CurrentPlayer.PlayerColor] >= card.GetCost(CurrentPlayer.PlayerColor) &&
                CurrentPlayer.GetHand().Any(h => h == card))
                CurrentPlayer.PlayLandCard(this, card);
        }

        public void PlayCard(ICard card)
        {
            if (CurrentPlayer.Energy[CurrentPlayer.PlayerColor] >= card.GetCost(CurrentPlayer.PlayerColor) &&
                CurrentPlayer.GetHand().Any(h => h == card)) ;
               // CurrentPlayer.PlayCard(this, card);
        }

        public void MoveCreature(ICard card)
        {

        }

    }
}
