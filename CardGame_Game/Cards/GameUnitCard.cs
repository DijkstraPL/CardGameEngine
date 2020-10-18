using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.Cards
{
    public abstract class GameUnitCard : GameCard, ICooldown, IAttacker, IHealthy
    {
        public int? BaseAttack { get; }
        public List<(Func<IAttacker, bool> conditon, int value)> AttackCalculators { get; } = new List<(Func<IAttacker, bool> conditon, int value)>();
        public List<(Func<IAttacker, bool> conditon, Func<IAttacker, int> value)> AttackFuncCalculators { get; } = new List<(Func<IAttacker, bool> conditon, Func<IAttacker, int> value)>();
        public int? FinalAttack => BaseAttack == null ? null :
            BaseAttack +
            AttackCalculators
            .Where(ac => ac.conditon(this))
            .Sum(ac => ac.value) +
            AttackFuncCalculators
            .Where(ac => ac.conditon(this))
            .Sum(ac => ac.value(this));

        public IHealthy AttackTarget { get; set; }

        public int? BaseCooldown { get; set; }

        private int? _cooldown;
        public int? Cooldown
        {
            get => _cooldown;
            set => _cooldown = value < 0 ? 0 : value;
        }

        public int? BaseHealth { get; }
        private List<(Func<IHealthy, bool> conditon, int value)> _healthCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
        public IEnumerable<(Func<IHealthy, bool> conditon, int value)> HealthCalculators => _healthCalculators;
        public int? FinalHealth
        {
            get
            {
                var finalHealth = BaseHealth == null ? null : BaseHealth + HealthCalculators.Where(ac => ac.conditon(this)).Sum(ac => ac.value);
                if (finalHealth <= 0)
                    Dead();
                return BaseHealth == null ? null : BaseHealth + HealthCalculators.Where(ac => ac.conditon(this)).Sum(ac => ac.value);
            }
        }

        public bool Contrattacked { get; set; }

        private bool _protectionUsed = false;

        public GameUnitCard(IPlayer owner, Card card, string name, string description, int? cost, InvocationTarget invocationTarget, int? attack, int? cooldown, int? health)
            : base(owner, card, name, description, cost, invocationTarget)
        {
            BaseAttack = attack;
            BaseCooldown = cooldown;
            Cooldown = BaseCooldown;
            BaseHealth = health;
        }

        public void SetAttackTarget(IHealthy attackTarget)
        {
            AttackTarget = attackTarget;
        }
        public void AddHealthCalculation((Func<IHealthy, bool> conditon, int value) calc)
        {
            if (Trait.HasFlag(Trait.Protection) && calc.value < 0 && !_protectionUsed && calc.conditon(this))
                _protectionUsed = true;
            else
                _healthCalculators.Add(calc);
        }

        private void Dead()
        {
            if (CardState == Enums.CardState.OnField)
            {
                CardState = Enums.CardState.OnGraveyard;
                Owner.BoardSide.Kill(this);
                Owner.AddToGraveyard(this);
            }
        }
    }

}
