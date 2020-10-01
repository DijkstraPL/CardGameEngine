using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Players.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules.Conditions
{
    /// <summary>
    /// Morale({'SELF'/'TARGET'},{Amount})
    /// </summary>
    [Export(Name, typeof(ICondition))]
    public class Cooldown : ICondition
    {
        public const string Name = "Cooldown";
        string ICondition.Name => Name;

        [ImportingConstructor]
        public Cooldown()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            GameCard card = null;
            if (args[0] == "SELF")
                card = gameEventArgs.SourceCard;
            if (Int32.TryParse(args[1], out int value) && card is ICooldown cooldown)
                return cooldown.Cooldown <= value;
            throw new ArgumentException(nameof(args));
        }
    }
}
