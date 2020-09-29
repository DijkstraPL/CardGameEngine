using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.Effects
{
    [Export(Name, typeof(IEffect))]
    public class AddAttack : IEffect
    {
        public const string Name = "AddAttack";
        string IEffect.Name => Name;

        [ImportingConstructor]
        public AddAttack()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, params string[] args)
        {
           if(args[0] == "SELF" && Int32.TryParse(args[1], out int value) && gameEventArgs.SourceCard is IAttacker attacker)
                attacker.AttackCalculators.Add(value);
        }
    }
}
