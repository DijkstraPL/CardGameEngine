using CardGame_Data.Entities.Enums;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;

namespace CardGame_Game.Players
{
    public class Player : IPlayer
    {
        public string Name { get; }
        public Stack<ILandCard> LandDeck { get; private set; }
        public Stack<ICard> Deck { get; private set; }
        public IList<ICard> Hand { get; private set; } = new List<ICard>();

        public CardColor PlayerColor { get; }

        public IDictionary<CardColor, int> Energy { get; } = new Dictionary<CardColor, int>
        {
            [CardColor.White] = 0,
            [CardColor.Red] = 0,
            [CardColor.Green] = 0,
        };

        public bool CardTaken { get; private set; } = false;
        public IBoardSide BoardSide { get; set; }

        public bool IsLandCardPlayed { get; set; }

        public event EventHandler<GameEventArgs> LandCardPlayed;

        public Player(string name, Stack<ICard> deck, Stack<ILandCard> landDeck)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            Name = name;
            Deck = deck ?? throw new ArgumentNullException(nameof(deck)); 
            LandDeck = landDeck ?? throw new ArgumentNullException(nameof(landDeck)); 
        }

        public void ShuffleDeck()
        {
            var deck = new Stack<ICard>(Deck);
            Deck = deck.Shuffle();
        }

        public void ShuffleLandDeck()
        {
            var landDeck = new Stack<ILandCard>(LandDeck);
            LandDeck = landDeck.Shuffle();
        }

        public void EndTurn()
        {
            Energy[PlayerColor] = 0;
            CardTaken = false;
            IsLandCardPlayed = false;
        }

        public void GetCardFromDeck()
        {
            CardTaken = true;
            Hand.Add(Deck.Pop());
        }

        public void GetCardFromLandDeck()
        {
            CardTaken = true;
            Hand.Add(LandDeck.Pop());
        }

        public void IncreaseEnergy(CardColor cardColor, int amount)
        {
            Energy[cardColor] += amount;
        }

        public IEnumerable<ICard> GetHand()
        {
            return Hand;
        }

        public void PlayLandCard(IGame game, ILandCard card)
        {
            if (IsLandCardPlayed)
                return;

            IsLandCardPlayed = true;
            Energy[PlayerColor] -= card.GetCost(PlayerColor);
            Hand.Remove(card);
            card.Play(game, this);
            BoardSide.AddLandCard(card);

            LandCardPlayed?.Invoke(this, new GameEventArgs { Card = card });
        }
    }
}
