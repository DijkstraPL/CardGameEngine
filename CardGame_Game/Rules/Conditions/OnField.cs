using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace CardGame_Game.Rules.Conditions
{
    /// <summary>
    /// OnField({'SELF'/'TARGET'})
    /// </summary>
    [Export(Name, typeof(ICondition))]
    public class OnField : ICondition
    {
        public const string Name = "OnField";
        string ICondition.Name => Name;

        [ImportingConstructor]
        public OnField()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            GameCard card = null;
            if (args[0] == "SELF")
                card = gameEventArgs.SourceCard;
            else if (args[0] == "TARGET")
                card = gameEventArgs.Targets.FirstOrDefault();
            return card?.CardState == CardState.OnField;
        }
    }
}
