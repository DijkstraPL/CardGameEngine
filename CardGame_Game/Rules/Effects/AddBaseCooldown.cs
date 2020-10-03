using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.Effects
{
    /// <summary>
    /// AddBaseCooldown({'TARGET'/'SELF'},{Amount})
    /// </summary>
    [Export(Name, typeof(IEffect))]
    public class AddBaseCooldown : IEffect
    {
        public const string Name = "AddBaseCooldown";
        string IEffect.Name => Name;

        [ImportingConstructor]
        public AddBaseCooldown()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args)
        {
            if (args[0] == "SELF" &&
                Int32.TryParse(args[1], out int value) &&
                gameEventArgs.SourceCard is ICooldown cooldown)
                cooldown.BaseCooldown += value;
        }
    }
}
