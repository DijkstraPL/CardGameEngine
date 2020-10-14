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
    /// HasNotTrait({'SELF'/'TARGET'},{TraitNumbers}...)
    /// </summary>
    [Export(Name, typeof(ICondition))]
    public class HasNotTrait : ICondition
    {
        public const string Name = "HasNotTrait";
        string ICondition.Name => Name;

        [ImportingConstructor]
        public HasNotTrait()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            if (args[0] == "TARGET")
            {
                var target = gameEventArgs.Targets.First();
                for (int i = 1; i < args.Length; i++)
                {
                    if (int.TryParse(args[i], out int traitNumber))
                    {
                        Trait trait = (Trait)traitNumber;
                        if (target.Trait.HasFlag(trait))
                            return false;
                    }
                    else
                        throw new ArgumentException();
                }

                return true;
            }
            throw new NotImplementedException();
        }
    }
}
