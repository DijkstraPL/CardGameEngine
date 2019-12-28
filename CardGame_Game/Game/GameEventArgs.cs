using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;

namespace CardGame_Game.Game
{
    public class GameEventArgs:EventArgs
    {
        public IGame Game { get; set; }
        public IPlayer Player { get; set; }
        public ILandCard Card { get; internal set; }
    }
}
