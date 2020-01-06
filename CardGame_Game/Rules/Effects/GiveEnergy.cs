using CardGame_Data.Entities.Enums;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules.Effects
{
    [Export(Name, typeof(IEffect))]
    public class GiveEnergy : IEffect
    {
        public const string Name = "GiveEnergy";

        private readonly IGame _game;

        [ImportingConstructor]
        public GiveEnergy([Import] IGame game)
        {
            _game = game;
        }

        public void Invoke(params string[] args)
        {
            CardColor cardColor = (args[0]) switch
            {
                ("white") => CardColor.White,
                ("red") => CardColor.Red,
                ("green") => CardColor.Green,
                _ => throw new NotImplementedException()
            };

            if (!Int32.TryParse(args[1], out int value))
                new ArgumentException(nameof(args));

            _game.CurrentPlayer.IncreaseEnergy(cardColor, value);
        }
    }
}
