using CardGame_DataAccess.Entities.Enums;
using Data = CardGame_Data.Data;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Linq;

namespace CardGame_DataAccess.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Trait Trait { get; set; }
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
        public int? CostBlue { get; set; }
        public int? CostRed { get; set; }
        public Rarity Rarity { get; set; }
        public CardColor Color { get; set; }
        public bool IsPublic { get; set; }
        public int SetId { get; set; }
        public Set Set { get; set; }

        public InvocationTarget InvocationTarget { get; set; }

        public Card()
        {
            Rules = new HashSet<CardRule>();
        }

        public static implicit operator Data.Card(Card card)
        {
            var dataCard = new Data.Card
            {
                Name = card.Name,
                CostBlue = card.CostBlue,
                Cooldown = card.Cooldown,
                Kind = (Data.Enums.Kind)card.Kind,
                InvocationTarget = (Data.Enums.InvocationTarget)card.InvocationTarget,
                Rarity = (Data.Enums.Rarity)card.Rarity,
                Description = card.Description,
                Color = (Data.Enums.CardColor)card.Color,
                Number = card.Number,
                Flavour = card.Flavour,
                CardType = card.CardType,
                Set = card.Set,
                SubType = card.SubType,
                Attack = card.Attack,
                CostGreen = card.CostGreen,
                CostRed = card.CostRed,
                Health = card.Health,
                Trait = (Data.Enums.Trait)card.Trait
            };
            foreach (var rule in card.Rules)
                dataCard.Rules.Add(rule.Rule);
            return dataCard;
        }
    }
}
