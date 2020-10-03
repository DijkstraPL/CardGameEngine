using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_DataAccess.Entities
{
    public class CardDeck
    {
        public int CardId { get; set; }
        public int DeckId { get; set; }
        public int Amount { get; set; }
        public Card Card { get; set; }
        public Deck Deck { get; set; }
    }
}
