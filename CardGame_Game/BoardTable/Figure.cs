using CardGame_Game.Cards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_Game.BoardTable
{
    public class Figure
    {
        public IUnitCard Card { get; }

        public int Attack => GetAttackValue();
        public int Cooldown => GetCooldownValue();
        public int Health => GetHealthValue();

        private IList<ICard> _equipments=new List<ICard>();
        public IEnumerable<ICard> Equipments => _equipments;

        private IList<ICard> _experiences = new List<ICard>();
        public IEnumerable<ICard> Experiences => _experiences;

        private IList<Func<int>> _attackCommands = new List<Func<int>>();
        private IList<Func<int>> _cooldownCommands = new List<Func<int>>();
        private IList<Func<int>> _healthCommands = new List<Func<int>>();

        public Figure(IUnitCard card)
        {
            Card = card;
        }

        public void AddEquipment(ICard card)
        {
            _equipments.Add(card);
        }
        public void AddExperience(ICard card)
        {
            _experiences.Add(card);
        }

        public void TurnPassed()
        {

        }

        private int GetAttackValue()
        {
            int attack = Card.Attack;
            attack += _attackCommands.Sum(ac => ac());
            return attack;
        }

        private int GetCooldownValue()
        {
            int cooldown = Card.Cooldown;
            cooldown += _cooldownCommands.Sum(ac => ac());
            return cooldown;
        }

        private int GetHealthValue()
        {
            int health = Card.Health;
            health += _healthCommands.Sum(ac => ac());
            return health;
        }
    }
}
