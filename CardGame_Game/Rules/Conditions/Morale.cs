using CardGame_Game.Cards;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Players;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace CardGame_Game.Rules.Conditions
{
    [Export(Name, typeof(ICondition))]
    public class Morale : ICondition
    {
        public const string Name = "Morale";
        string ICondition.Name => Name;

        [ImportingConstructor]
        public Morale()
        {
        }

        public bool Validate(GameEventArgs gameEventArgs, params string[] args)
        {
            if (args[0] == "SELF" && 
                Int32.TryParse(args[1], out int value) && 
                gameEventArgs.Player is BluePlayer bluePlayer)
                    return bluePlayer.Morale >= value;
            return false;
        }
    }
}
