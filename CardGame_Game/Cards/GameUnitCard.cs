using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Players.Interfaces;
using CardGame_Game.Rules;
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

        protected GameUnitCard _gameUnitInitState => _initState as GameUnitCard;

        public GameUnitCard(IPlayer owner, Card card, string name, string description, int? cost, InvocationTarget invocationTarget, int? attack, int? cooldown, int? health)
            : base(owner, card, name, description, cost, invocationTarget)
        {
            BaseAttack = attack;
            BaseCooldown = cooldown;
            Cooldown = BaseCooldown;
            BaseHealth = health;
        }

        protected GameUnitCard(GameUnitCard gameUnitCard) : base(gameUnitCard)
        {
            BaseAttack = gameUnitCard.BaseAttack;
            BaseCooldown = gameUnitCard.BaseCooldown;
            Cooldown = gameUnitCard.Cooldown;
            BaseHealth = gameUnitCard.BaseHealth;

            foreach (var attackCalculator in gameUnitCard.AttackCalculators)
                AttackCalculators.Add(attackCalculator);
            foreach (var attackCalculator in gameUnitCard.AttackFuncCalculators)
                AttackFuncCalculators.Add(attackCalculator);

            foreach (var healthCalculator in gameUnitCard.HealthCalculators)
                _healthCalculators.Add(healthCalculator);
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

        public override void Reset()
        {
            base.Reset();
            //BaseAttack = _gameUnitInitState.BaseAttack;
            BaseCooldown = _gameUnitInitState.BaseCooldown;
            Cooldown = _gameUnitInitState.Cooldown;
            //BaseHealth = _gameUnitInitState.BaseHealth;

            AttackTarget = null;

            Contrattacked = false;
            _protectionUsed = false;

            AttackCalculators.Clear();
            foreach (var attackCalculator in _gameUnitInitState.AttackCalculators)
                AttackCalculators.Add(attackCalculator);

            AttackFuncCalculators.Clear();
            foreach (var attackCalculator in _gameUnitInitState.AttackFuncCalculators)
                AttackFuncCalculators.Add(attackCalculator);

            foreach (var healthCalculator in _gameUnitInitState.HealthCalculators)
                _healthCalculators.Add(healthCalculator);
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
