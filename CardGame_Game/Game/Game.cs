using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using CardGame_Game.Players.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;

namespace CardGame_Game.Game
{
    public class Game : IGame
    {
        public int TurnCounter { get; private set; } = 1;
        public TurnTimer TurnTimer { get; }

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

            TurnTimer = new TurnTimer();
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

            _firstPlayer.SaveInitData();
            _secondPlayer.SaveInitData();

            GameEventsContainer.GameStartedEvent.Raise(this, new GameEventArgs { Game = this });

            TurnTimer.Start();
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
            TurnTimer.Reset();
        }

        public bool GetCardFromDeck()
        {
            if (!CurrentPlayer.CardTaken)
                return CurrentPlayer.GetCardFromDeck();
            return false;
        }

        public bool GetCardFromLandDeck()
        {
            if (!CurrentPlayer.CardTaken)
                return CurrentPlayer.GetCardFromLandDeck();
            return false;
        }

        public bool PlayCard(GameCard card, InvocationData invocationData)
        {
            if (card != null &&
                CurrentPlayer.Energy >= card.Cost &&
                CurrentPlayer.Hand.Any(h => h == card) &&
                card.CanBePlayed(this, CurrentPlayer, invocationData))
            {
                card.Play(this, CurrentPlayer, invocationData);
                GameEventsContainer.CardPlayedEvent.Raise(card, new GameEventArgs { Game = this, Player = CurrentPlayer, SourceCard = card });
                return true;
            }
            return false;
        }

        public bool IsGameFinished()
            => CurrentPlayer.FinalHealth <= 0 || NextPlayer.FinalHealth <= 0;

        public void SendCardToHand(GameCard card, IPlayer player)
        {
            card.CardState = CardState.InHand;
            card.Reset();
            var fieldCard = Board.LeftBoardSite.Fields
                .Concat(Board.RightBoardSite.Fields)
                .FirstOrDefault(f => f.Card == card);
            if (fieldCard != null)
                fieldCard.Card = null;
            player.Hand.Add(card);
        }

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
