using CardGame_Data.Entities.Enums;
using System.Collections.Generic;

namespace CardGame_Data.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Trait { get; set; }
        public ICollection<CardRule> Rules { get; private set; }
        public string Flavour { get; set; }
        public Kind Kind { get; set; }
        public int CardTypeId { get; set; }
        public CardType CardType { get; set; }
        public int? SubTypeId { get; set; }
        public Subtype SubType { get; set; }
        public int? Attack { get; set; }
        public int? Cooldown { get; set; }
        public int? Health { get; set; }
        public int? CostGreen { get; set; }
        public int? CostWhite { get; set; }
        public int? CostRed { get; set; }
        public Rarity Rarity { get; set; }
        public CardColor Color { get; set; }
        public bool IsPublic { get; set; }
        public int Set { get; set; }

        public Card()
        {
            Rules = new HashSet<CardRule>();
        }
    }
}
