using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace CardGame_Game.Rules.Effects
{
    /// <summary>
    /// AddCooldown({'TARGET'/'SELF'},{Amount})
    /// </summary>
    [Export(Name, typeof(IEffect))]
    public class AddCooldown : IEffect
    {
        public const string Name = "AddCooldown";
        string IEffect.Name => Name;

        [ImportingConstructor]
        public AddCooldown()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args)
        {
            if (args[0] == "TARGET" && 
                Int32.TryParse(args[1], out int value) && 
                gameEventArgs.Targets.FirstOrDefault() is ICooldown cooldown)
                    cooldown.Cooldown += value;
        }
    }
}
