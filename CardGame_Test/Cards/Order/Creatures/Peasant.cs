using CardGame_Test.Cards.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Test.Cards.Order.Creatures
{
    public class Peasant : CreatureCard, IMoraleEffect
    {
        public int RequiredMorale => 2;

        public Peasant() 
            : base("Peasant", attack: 2, reactionTime: 2, health: 2, move: 1, cost: 1, type: Types.Order,
                  CardTypes.Human, new List<CardSubtypes> { Enums.CardSubtypes.None })
        {
        }

        public void IncludeMoraleEffect()
        {
            Attack += 1;
        }
    }
}
