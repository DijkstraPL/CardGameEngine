using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.Conditions
{
    /// <summary>
    /// TimesPerTurn({keyName},{amount})
    /// </summary>
    [Export(Name, typeof(ICondition))]
    public class TimesPerTurn : ICondition
    {
        public const string Name = "TimesPerTurn";
        string ICondition.Name => Name;

        private static IDictionary<string, IDictionary<int, int>> _counters = new Dictionary<string, IDictionary<int, int>>();

        [ImportingConstructor]
        public TimesPerTurn()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            if (!_counters.ContainsKey(args[0]))
                _counters.Add(args[0], new Dictionary<int, int>());

            var specificCounter = _counters[args[0]];

            if (int.TryParse(args[1], out int maxTimes))
            {
                if (!specificCounter.ContainsKey(gameEventArgs.Game.TurnCounter))
                    specificCounter.Add(gameEventArgs.Game.TurnCounter, 0);
                else
                    specificCounter[gameEventArgs.Game.TurnCounter] += 1;

                return specificCounter[gameEventArgs.Game.TurnCounter] < maxTimes;
            }
            return false;
        }
    }
}
