﻿using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.Players
{
    public abstract class Player : IPlayer
    {
        public string Name { get; }
        public IGameEventsContainer GameEventsContainer { get; }
        public Stack<GameCard> LandDeck { get; private set; }
        public Stack<GameCard> Deck { get; private set; }
        public Stack<GameCard> Graveyard { get; } = new Stack<GameCard>();
        public IList<GameCard> Hand { get; private set; } = new List<GameCard>();

        public CardColor PlayerColor { get; protected set; }
        public int Energy { get; private set; }

        public bool CardTaken { get; private set; } = false;
        public IBoardSide BoardSide { get; set; }

        public bool IsLandCardPlayed { get; set; }

        public int? BaseHealth { get; private set; } = 2;
        private List<(Func<IHealthy, bool> conditon, int value)> _healthCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
        public IEnumerable<(Func<IHealthy, bool> conditon, int value)> HealthCalculators => _healthCalculators;
        public int? FinalHealth
        {
            get
            {
                var finalHealth = BaseHealth == null ? null : BaseHealth + HealthCalculators.Where(ac => ac.conditon(this)).Sum(ac => ac.value);
                if (finalHealth <= 0)
                    IsLoser = true;

                return BaseHealth == null ? null : BaseHealth + HealthCalculators.Where(ac => ac.conditon(this)).Sum(ac => ac.value);
            }
        }

        public bool IsLoser { get; private set; }
        public bool Contrattacked { get; set; }

        public IEnumerable<GameCard> AllCards { get; private set; }

        private readonly Stack<Card> _landDeck;
        private readonly GameCardFactory _gameCardFactory;
        private readonly Stack<Card> _deck;
        private IGame _game;

        public Player(string name, Stack<Card> deck, Stack<Card> landDeck, GameCardFactory gameCardFactory, IGameEventsContainer gameEventsContainer)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            Name = name;
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
            _landDeck = landDeck ?? throw new ArgumentNullException(nameof(landDeck));
            _gameCardFactory = gameCardFactory ?? throw new ArgumentNullException(nameof(gameCardFactory));
            GameEventsContainer = gameEventsContainer;
        }

        public void PrepareForGame()
        {
            var organizedDeck = _deck.Where(c => c.Kind != Kind.Land).Select(c => _gameCardFactory.CreateGameCard(this, c));
            Deck = new Stack<GameCard>(organizedDeck).Shuffle();
            var organizedLandDeck = _landDeck.Where(c => c.Kind == Kind.Land).Select(c => _gameCardFactory.CreateGameCard(this, c));
            LandDeck = new Stack<GameCard>(organizedLandDeck).Shuffle();

            AllCards = new List<GameCard>(Deck.Concat(LandDeck));
        }

        public void RegisterTriggers(IGame game)
        {
            _game = game;
            foreach (var card in Deck.Concat(LandDeck))
                card.RegisterTriggers(game);
        }

        public void EndTurn()
        {
            Energy = 0;
            CardTaken = false;
            IsLandCardPlayed = false;
        }

        public bool GetCardFromDeck()
        {
            try
            {
                CardTaken = true;
                var card = Deck.Pop();
                card.CardState = CardState.InHand;
                Hand.Add(card);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool GetCardFromLandDeck()
        {
            try
            {
                CardTaken = true;
                var card = LandDeck.Pop();
                card.CardState = CardState.InHand;
                Hand.Add(card);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public void SetStartingHand()
        {
            for (int i = 0; i < 3; i++)
            {
                var card = LandDeck.Pop();
                card.CardState = CardState.InHand;
                Hand.Add(card);
            }

            for (int i = 0; i < 4; i++)
            {
                var card = Deck.Pop();
                card.CardState = CardState.InHand;
                Hand.Add(card);
            }
        }

        public void IncreaseEnergy(CardColor cardColor, int value)
        {
            if (PlayerColor == cardColor)
                Energy += value;
        }

        public void AddToGraveyard(GameCard card)
        {
            Graveyard.Push(card);
        }

        public virtual void OnCardMove()
        {
            IncreaseEnergy(PlayerColor, -1);
        }

        public virtual bool CanMove()
        {
            return Energy > 0;
        }

        public void AddHealthCalculation((Func<IHealthy, bool> conditon, int value) calc)
        {
                _healthCalculators.Add(calc);
        }
    }
}
