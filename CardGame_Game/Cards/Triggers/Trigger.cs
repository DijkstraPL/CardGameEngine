using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_Game.Cards.Triggers
{
    public class Trigger : ITrigger
    {
        private IList<ICondition> _conditions = new List<ICondition>();
        public IEnumerable<ICondition> Conditions => _conditions;
        private IList<IEffect> _effects = new List<IEffect>();
        public IEnumerable<IEffect> Effects => _effects;


        public void AddEvent(ICondition condition, IEffect effect)
        {
            _conditions.Add(condition);
            _effects.Add(effect);
        }

        public void TriggerIt(object sender, GameEventArgs eventArgs)
        {
            if (Conditions.All(c => c.Validate(eventArgs.Game)))
                Effects.ToList().ForEach(e => e.Invoke(eventArgs.Game));
        }
    }
}