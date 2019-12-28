using System;
using System.Collections.Generic;
using System.Text;
using CardGame_Test.Cards.Enums;

namespace CardGame_Test.Cards
{
    public abstract class SpellCard : Card
    {
        public SpellCard(string name, int cost, Types type,
            CardTypes cardType, IList<CardSubtypes> cardSubtypes)
            : base(name, cost, type, cardType, cardSubtypes)
        {
        }
    }
}
