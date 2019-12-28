using CardGame_Test.BoardTable;
using CardGame_Test.Cards;
using CardGame_Test.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Test.Units
{
    public class CreatureUnit : Unit, ITurn, ICreatureUnit
    {
        #region Properties
        
    

        #endregion // Properties


        public CreatureUnit(CreatureCard creature) : base(creature)
        {
        }

        public void StartTurn()
        {
            if (CurrentReactionTime > 0)
                CurrentReactionTime -= 1;
        }

        public void EndTurn()
        {
        }

        public void AttachEnchantment(IEnchantmnentEffect enchantmnentEffect)
        {
            Enchantments.Add(enchantmnentEffect);
        }

        public void Attack(Field field)
        {
            if (CurrentReactionTime != 0)
                return;

            field.Unit.CurrentHealth -= this.CurrentAttack;

            Creature.OnAfterAttack();
            CurrentReactionTime = BaseReactionTime;
        }
    }

    public interface ITurn
    {
        void StartTurn();
        void EndTurn();
    }
}
