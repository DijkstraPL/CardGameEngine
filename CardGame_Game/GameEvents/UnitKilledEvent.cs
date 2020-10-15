using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class UnitKilledEvent : GameEvent
    {
        public override string Name => "UnitKilled";

        public event EventHandler<GameEventArgs> TurnStarting;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            TurnStarting?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            TurnStarting += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
