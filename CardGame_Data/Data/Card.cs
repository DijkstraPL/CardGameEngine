using CardGame_Data.Data.Enums;
using System.Collections.Generic;

namespace CardGame_Data.Data
{
    public class Card
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Trait Trait { get; set; }
        public ICollection<Rule> Rules { get; private set; }
        public string Flavour { get; set; }
        public Kind Kind { get; set; }
        public CardType CardType { get; set; }
        public Subtype SubType { get; set; }
        public int? Attack { get; set; }
        public int? Cooldown { get; set; }
        public int? Health { get; set; }

        public int? CostGreen { get; set; }
        public int? CostBlue { get; set; }
        public int? CostRed { get; set; }
        public Rarity Rarity { get; set; }
        public CardColor Color { get; set; }
        public Set Set { get; set; }
        public string Rule { get; set; }

        public InvocationTarget InvocationTarget { get; set; }

        public Card()
        {
            Rules = new HashSet<Rule>();
        }
    }
}
