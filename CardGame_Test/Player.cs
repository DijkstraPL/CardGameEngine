using CardGame_Test.BoardTable;
using CardGame_Test.Cards;
using CardGame_Test.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Test
{
    public class Player
    {
        public BoardSite BoardSite { get; set; }
        public int CurrentMana { get; private set; } = 0;
        public Stack<Card> Deck { get; }
        public Stack<Card> Graveyard { get; }
        public Stack<LandCard> LandDeck { get;  }
        public IList<Card> Hand { get; }

        public string Name { get; }

        public bool CardTaken { get; private set; }

        public Player(string name, Stack<Card> deck, Stack<LandCard> landDeck)
        {
            Name = name;
            Deck = deck.Shuffle();
            LandDeck = landDeck.Shuffle();

            Hand = new List<Card>();
        }

        public IEnumerable<Card> GetHand()
        {
            return Hand;
        }
        public void NextTurn()
        {
            CardTaken = false;
        }

        public void TakeCardFromDeck()
        {
            if (Deck.Count == 0)
                return;
            Hand.Add(Deck.Pop());
            CardTaken = true;
        }


        public void TakeCardFromLandDeck()
        {
            if (LandDeck.Count == 0)
                return;
            Hand.Add(LandDeck.Pop());
            CardTaken = true;
        }

        public void PutCreatureIntoField(Field field, CreatureCard creature)
        {
            field.Unit = new CreatureUnit(creature);
        }

        public void RefreshMana(int amount)
        {
            CurrentMana = amount;
        }
    }
}
