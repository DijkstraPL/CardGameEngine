using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CardGame_Data.Data
{
    public class Deck
    {
        public string Name { get; set; }
        public ICollection<DeckCard> Cards { get; set; }

        public Deck()
        {
            Cards = new Collection<DeckCard>();
        }
    }
}
