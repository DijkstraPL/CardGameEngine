using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_DataAccess.Entities
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CardDeck> Cards { get; set; }
    }
}
