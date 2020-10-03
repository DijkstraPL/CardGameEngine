using CardGame_Game.Cards;
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
    /// Controls({'SELF'},{'Card name'},{Amount})
    /// </summary>
    [Export(Name, typeof(ICondition))]
    public class Controls : ICondition
    {
        public const string Name = "Controls";
        string ICondition.Name => Name;

        [ImportingConstructor]
        public Controls()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            if (args[0] == "SELF" &&
                Int32.TryParse(args[2], out int minAmount))
            {
                var filteredFieldCards = gameEventArgs.Player.BoardSide.Fields.Where(f => f.Card?.Name == args[1]);
                var filteredLandCards =  gameEventArgs.Player.BoardSide.LandCards.Where(lc => lc.Name == args[1]);
                return filteredFieldCards.Count() + filteredLandCards.Count() >= minAmount;
            }
            throw new NotImplementedException();
        }
    }
}

