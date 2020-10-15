using CardGame_Data.Data.Enums;
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
    /// IsKind({'SELF'/'TARGET'},{'CREATURE','STRUCTURE'})
    /// </summary>
    [Export(Name, typeof(ICondition))]
    public class IsKind : ICondition
    {
        public const string Name = "IsKind";
        string ICondition.Name => Name;

        [ImportingConstructor]
        public IsKind()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            if (args[0] == "TARGET")
            {
                var target = gameEventArgs.Targets.First();

                if (args[1] == "CREATURE")
                    return target.Kind == Kind.Creature;
                if (args[2] == "STRUCTURE")
                    return target.Kind == Kind.Structure;
            }
            throw new NotImplementedException();
        }
    } 
}
