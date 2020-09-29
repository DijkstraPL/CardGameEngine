using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;

namespace CardGame_Game.Game
{
    public class GameEventArgs:EventArgs
    {
        public IGame Game { get; set; }
        public IPlayer Player { get; set; }
        public GameCard SourceCard { get; internal set; }
        public IEnumerable<GameCard> Targets { get; internal set; }
    }
}
