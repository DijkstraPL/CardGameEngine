using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Players.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules.Conditions
{
    [Export(Name, typeof(ICondition))]
    [ExportMetadata(When, true)]
    public class Countdown : ICondition
    {
        public const string Name = "Countdown";
        public const string When = "TurnStarted";

        public string Happend => When;

        private readonly ILandCard _card;

        [ImportingConstructor]
        public Countdown([Import] ILandCard card)
        {
            _card = card;
        }

        public bool Validate(params string[] args)
        {
            if (Int32.TryParse(args[0], out int value))
                return _card.Countdown <= value;
            throw new ArgumentException(nameof(args));
        }
    }
}
