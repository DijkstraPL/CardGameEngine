using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Cards.Interfaces
{
    public interface IAttacker
    {
        int? BaseAttack { get; }
        List<(Func<IAttacker, bool> conditon, int value)> AttackCalculators { get; }
        List<(Func<IAttacker, bool> conditon, Func<IAttacker,int> value)> AttackFuncCalculators { get; }
        int? FinalAttack { get; }

        public IHealthy AttackTarget { get; set; }
    }
}
