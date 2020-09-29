using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Cards.Interfaces
{
    public interface IAttacker
    {
        int? BaseAttack { get; }
        List<int> AttackCalculators { get; }
        int? FinalAttack { get; }
    }
}
