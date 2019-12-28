using System;
using System.Collections.Generic;
using System.Text;
using CardGame_Test.Cards.Enums;
using CardGame_Test.Units.Interfaces;

namespace CardGame_Test.Cards.Order.Spells
{
    public class HasteBlessing : SpellCard, ITargetCreatureEffect
    {
        public HasteBlessing()
            : base("Haste blessing", 1, Types.Order, CardTypes.Spell, 
                  new List<CardSubtypes> { Enums.CardSubtypes.Transformations })
        {
        }

        public void Cast(ICreatureUnit creatureUnit)
        {
            creatureUnit.CurrentReactionTime -= 1;
        }
    }

    internal interface ITargetCreatureEffect
    {
        void Cast(ICreatureUnit creatureUnit);
    }
}
