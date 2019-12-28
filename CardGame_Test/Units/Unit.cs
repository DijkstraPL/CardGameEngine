using CardGame_Test.Cards;
using System.Collections.Generic;

namespace CardGame_Test.Units
{
    public abstract class Unit
    {
        public int BaseAttack => _creature.Attack;
        private int _currentAttack;
        public int CurrentAttack
        {
            get => _currentAttack;
            set { _currentAttack = value; }
        }

        public int BaseReactionTime => _creature.ReactionTime;

        private int _currentReactionTime;
        public int CurrentReactionTime
        {
            get => _currentReactionTime;
            set { _currentReactionTime = value; }
        }
        public int BaseHealth => _creature.Health;
        private int _currentHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
            set { _currentHealth = value; }
        }
        public int BaseMove => _creature.Move;
        private int _currentMove;
        public int CurrentMove
        {
            get => _currentMove;
            set { _currentMove = value; }
        }

        public IList<IEnchantmnentEffect> Enchantments { get; }

        public CreatureCard Creature { get; }

        public Unit(CreatureCard creature)
        {
            Enchantments = new List<IEnchantmnentEffect>();

            Creature = creature;

            _currentAttack = BaseAttack;
            _currentReactionTime = BaseReactionTime;
            _currentHealth = BaseHealth;
            _currentMove = BaseMove;
        }
    }
}
