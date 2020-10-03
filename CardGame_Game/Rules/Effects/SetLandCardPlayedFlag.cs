using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.Effects
{
    /// <summary>
    /// SetLandCardPlayedFlag({'OWNHERO'},{'FALSE'})
    /// </summary>
    [Export(Name, typeof(IEffect))]
    public class SetLandCardPlayedFlag : IEffect
    {
        public const string Name = "SetLandCardPlayedFlag";
        string IEffect.Name => Name;

        [ImportingConstructor]
        public SetLandCardPlayedFlag()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args)
        {
            if (args[0] == "OWNHERO")
            {
                if (args[1] == "FALSE")
                    gameEventArgs.Player.IsLandCardPlayed = false;
            }
        }
    }
}
