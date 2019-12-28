using CardGame_Test.Cards.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Test.Cards.Order.Creatures
{
    public class OldKnight : CreatureCard
    {
        public OldKnight() : base("Old Knight", 
            attack: 2, reactionTime: 1, health: 2, move: 1, cost: 1, type: Types.Order,
            CardTypes.Human, new List<CardSubtypes> { Enums.CardSubtypes.Knight })
        {
            AfterAttack += OldKnight_AfterAttack;
        }

        ~OldKnight()
        {
            AfterAttack -= OldKnight_AfterAttack;
        }

        private void OldKnight_AfterAttack(object sender, EventArgs e)
        {
            ReactionTime += 1;
        }
    }
}
