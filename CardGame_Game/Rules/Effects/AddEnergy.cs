using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;

namespace CardGame_Game.Rules.Effects
{
    /// <summary>
    /// AddEnergy({'BLUE'/'RED'/'GREEN'},{Amount})
    /// </summary>
    [Export(Name, typeof(IEffect))]
    public class AddEnergy : IEffect
    {
        public const string Name = "AddEnergy";
        string IEffect.Name => Name;

        [ImportingConstructor]
        public AddEnergy()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args)
        {
            CardColor cardColor = (args[0]) switch
            {
                ("BLUE") => CardColor.Blue,
                ("RED") => CardColor.Red,
                ("GREEN") => CardColor.Green,
                _ => throw new NotImplementedException()
            };

            if (!Int32.TryParse(args[1], out int value))
                new ArgumentException(nameof(args));

            gameEventArgs.Game.CurrentPlayer.IncreaseEnergy(cardColor, value);
        }
    }
}
