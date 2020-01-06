using CardGame_Game.Game.Interfaces;
using System.Collections.Generic;

namespace CardGame_Game.Cards.Interfaces
{
    public interface IUnitCard : ICard
    {
        //Element Element { get; }
        int Attack { get; }
        int Cooldown { get; }
        int Health { get; }

        IEnumerable<ITrait> Traits { get; }

        //void OnAttacked(object sender, EventArgs e);
        //void OnDefensed(object sender, EventArgs e);
        //void OnBattlefieldEntered(object sender, EventArgs e);

        //void OnNeighboursEffect(object sender, EventArgs e);
        //void OnTurnStarted(object sender, EventArgs e);
        //void OnTriggered(object sender, EventArgs e);
    }
}
