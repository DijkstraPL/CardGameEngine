using CardGame_Test.Cards.Enums;
using System.Collections.Generic;

namespace CardGame_Test.Cards
{
    public abstract class Card
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public Types Type { get; set; }
        public CardTypes CardType { get; set; }
        public IList<CardSubtypes> CardSubtypes { get; set; }

        public Card(string name, int cost, Types type,
            CardTypes cardType, IList<CardSubtypes> cardSubtypes)
        {
            Name = name;
            Cost = cost;
            Type = type;
            CardType = cardType;
            CardSubtypes = cardSubtypes;
        }
    }
}
