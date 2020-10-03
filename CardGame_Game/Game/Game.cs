using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using CardGame_Game.Players.Interfaces;
using System;
using System.Linq;

namespace CardGame_Game.Game
{

    public class Game : IGame
    {
        public int TurnCounter { get; private set; }
        public IBoard Board { get; }

        public IPlayer CurrentPlayer { get; private set; }
        public IPlayer NextPlayer { get; private set; }

        public IGameEventsContainer GameEventsContainer { get; }

        private readonly IPlayer _firstPlayer;
        private readonly IPlayer _secondPlayer;
        private readonly IRandomHelper _randomHelper;

        public Game(IPlayer firstPlayer, IPlayer secondPlayer, IBoard board, IRandomHelper randomHelper, IGameEventsContainer gameEventsContainer)
        {
            _firstPlayer = firstPlayer ?? throw new ArgumentNullException(nameof(firstPlayer));
            _secondPlayer = secondPlayer ?? throw new ArgumentNullException(nameof(secondPlayer));
            Board = board ?? throw new ArgumentNullException(nameof(board));
            _randomHelper = randomHelper ?? throw new ArgumentNullException(nameof(randomHelper));
            GameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));

            _firstPlayer.BoardSide = Board.LeftBoardSite;
            _secondPlayer.BoardSide = Board.RightBoardSite;
        }

        public void StartGame()
        {
            GameEventsContainer.GameStartingEvent.Raise(this, new GameEventArgs { Game = this });

            _firstPlayer.PrepareForGame();
            _secondPlayer.PrepareForGame();

            _firstPlayer.RegisterTriggers(this);
            _secondPlayer.RegisterTriggers(this);

            _firstPlayer.SetStartingHand();
            _secondPlayer.SetStartingHand();

            SetPlayerOrder();

            GameEventsContainer.PlayerInitializedEvent.Raise(this, new GameEventArgs { Game = this, Player = CurrentPlayer });
            GameEventsContainer.PlayerInitializedEvent.Raise(this, new GameEventArgs { Game = this, Player = NextPlayer });

            GameEventsContainer.GameStartedEvent.Raise(this, new GameEventArgs { Game = this });
        }

        public void FinishTurn()
        {
            CurrentPlayer.BoardSide.FinishTurn(this, CurrentPlayer);

            GameEventsContainer.TurnFinishedEvent.Raise(this, new GameEventArgs { Game = this, Player = CurrentPlayer });
            NextTurn();
        }

        private void NextTurn()
        {
            CurrentPlayer.EndTurn();
            TurnCounter++;

            if (_firstPlayer == CurrentPlayer)
            {
                NextPlayer = _firstPlayer;
                CurrentPlayer = _secondPlayer;
            }
            else
            {
                NextPlayer = _secondPlayer;
                CurrentPlayer = _firstPlayer;
            }

            GameEventsContainer.TurnStartingEvent.Raise(this, new GameEventArgs { Game = this, Player = CurrentPlayer });

            CurrentPlayer.BoardSide.StartTurn(this);

            GameEventsContainer.TurnStartedEvent.Raise(this, new GameEventArgs { Game = this, Player = CurrentPlayer });
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

        public void PlayCard(GameCard card, InvocationData invocationData)
        {
            if (CurrentPlayer.Energy >= card.Cost &&
                CurrentPlayer.Hand.Any(h => h == card) &&
                card.CanBePlayed(this, CurrentPlayer, invocationData))
            {
                card.Play(this, CurrentPlayer, invocationData);
                GameEventsContainer.CardPlayedEvent.Raise(card, new GameEventArgs { Game = this, Player = CurrentPlayer, SourceCard = card });
            }
        }

        public bool IsGameFinished()
            => CurrentPlayer.HitPoints <= 0 || NextPlayer.HitPoints <= 0;

        private void SetPlayerOrder()
        {
            if (_randomHelper.FlipCoin())
            {
                CurrentPlayer = _firstPlayer;
                NextPlayer = _secondPlayer;
            }
            else
            {
                CurrentPlayer = _secondPlayer;
                NextPlayer = _firstPlayer;
            }
        }
    }
}
