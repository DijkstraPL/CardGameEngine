using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.Conditions
{
    [Export(Name, typeof(ICondition))]
    public class Owner : ICondition
    {
        public const string Name = "Owner";
        string ICondition.Name => Name;

        [ImportingConstructor]
        public Owner()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            if (args[0] == "SELF")
            {
                var owner = gameEventArgs.SourceCard.Owner;
                return owner == gameEventArgs.Player;
            }
            return false;
        }
    }
}
