using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Players.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.Cards
{
    public abstract class GameUnitCard : GameCard, ICooldown, IAttacker
    {
        public int? BaseAttack { get; }
        public List<int> AttackCalculators { get; } = new List<int>();
        public int? FinalAttack => BaseAttack == null ? null : BaseAttack + AttackCalculators.Sum();

        public int? BaseCooldown { get; }
        public int? Cooldown { get; set; }
        public int? Health { get; }

        public GameUnitCard(IPlayer owner, Card card, int id, string name, string description, int? cost, InvocationTarget invocationTarget, int? attack, int? cooldown, int? health)
            : base(owner, card, id, name, description, cost, invocationTarget)
        {
            BaseAttack = attack;
            BaseCooldown = cooldown;
            Cooldown = BaseCooldown;
            Health = health;
        }
    }

}
