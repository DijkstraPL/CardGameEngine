using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.Effects
{
    /// <summary>
    /// Heal({'OWNHERO'},{Amount})
    /// </summary>
    [Export(Name, typeof(IEffect))]
    public class Heal : IEffect
    {
        public const string Name = "Heal";
        string IEffect.Name => Name;

        [ImportingConstructor]
        public Heal()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args)
        {
            if (args[0] == "OWNHERO" &&
                Int32.TryParse(args[1], out int value))
            {
                if (gameEventArgs.Player.HitPoints + value > gameEventArgs.Player.MaxHitPoints)
                    gameEventArgs.Player.HitPoints = gameEventArgs.Player.MaxHitPoints;
                else
                    gameEventArgs.Player.HitPoints += value;
            }
        }
    }
}
