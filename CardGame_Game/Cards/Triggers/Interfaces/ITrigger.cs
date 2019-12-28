using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Cards.Triggers.Interfaces
{
    public interface ITrigger
    {
        IEnumerable<ICondition> Conditions { get; }
        IEnumerable<IEffect> Effects { get; }

        void AddEvent(ICondition condition, IEffect effect);
        void TriggerIt(object sender, GameEventArgs eventArgs);
    }
}
