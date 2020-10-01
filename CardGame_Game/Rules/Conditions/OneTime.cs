using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.Conditions
{
    /// <summary>
    /// Times({amount})
    /// </summary>
    [Export(Name, typeof(ICondition))]
    public class Times : ICondition
    {
        public const string Name = "Times";
        string ICondition.Name => Name;

        private int _counter = 0;

        [ImportingConstructor]
        public Times()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            if (int.TryParse(args[0], out int maxTimes) && _counter < maxTimes)
            {
                _counter++;
                return true;
            }
            return false;
        }
    }
}
