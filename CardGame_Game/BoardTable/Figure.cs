using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_Game.BoardTable
{
    public class Figure
    {
        public GameUnitCard Card { get; }

        public int Attack => GetAttackValue();
        public int Cooldown => GetCooldownValue();
        public int Health => GetHealthValue();


        private IList<Func<int>> _attackCommands = new List<Func<int>>();
        private IList<Func<int>> _cooldownCommands = new List<Func<int>>();
        private IList<Func<int>> _healthCommands = new List<Func<int>>();

        public Figure(GameUnitCard card)
        {
            Card = card;
        }

        public void TurnPassed()
        {

        }

        private int GetAttackValue()
        {
            int attack = Card.BaseAttack ?? 0;
            attack += _attackCommands.Sum(ac => ac());
            return attack;
        }

        private int GetCooldownValue()
        {
            int cooldown = Card.BaseCooldown ?? 0;
            cooldown += _cooldownCommands.Sum(ac => ac());
            return cooldown;
        }

        private int GetHealthValue()
        {
            int health = Card.BaseHealth ?? 0;
            health += _healthCommands.Sum(ac => ac());
            return health;
        }
    }
}
