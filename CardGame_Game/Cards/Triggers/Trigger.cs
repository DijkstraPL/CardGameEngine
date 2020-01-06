using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_Game.Cards.Triggers
{
    public class Trigger
    {
        private IList<ICondition> _conditions = new List<ICondition>();
        public IEnumerable<ICondition> Conditions => _conditions;

        private IList<IEffect> _effects = new List<IEffect>();
        public IEnumerable<IEffect> Effects => _effects;

        public Trigger(ICondition condition, IEffect effect)
        {
            _conditions.Add(condition);
            _effects.Add(effect);
        }
    }
}