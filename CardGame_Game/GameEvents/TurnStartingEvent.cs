using CardGame_Game.Game;
using System;

namespace CardGame_Game.GameEvents
{
    public class TurnStartingEvent : GameEvent
    {
        public override string Name => "TurnStarting";

        public event EventHandler<GameEventArgs> TurnStarting;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            TurnStarting?.Invoke(source, gameEventArgs);
        }

        public override void Add(Action<GameEventArgs> action)
        {
            TurnStarting += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
