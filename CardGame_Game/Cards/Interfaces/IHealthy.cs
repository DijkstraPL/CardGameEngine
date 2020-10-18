using System;
using System.Collections.Generic;

namespace CardGame_Game.Cards.Interfaces
{
    public interface IHealthy
    {
        int? BaseHealth { get; }
        IEnumerable<(Func<IHealthy,bool> conditon, int value)> HealthCalculators { get; } 
        int? FinalHealth { get; }
        bool Contrattacked { get; set; }

        void AddHealthCalculation((Func<IHealthy, bool> conditon, int value) calc);
    }
}
