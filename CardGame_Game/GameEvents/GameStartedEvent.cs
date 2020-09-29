using CardGame_Game.Game;
using System;

namespace CardGame_Game.GameEvents
{
    public class GameStartedEvent : GameEvent
    {
        public override string Name => "GameStarted";

        public event EventHandler<GameEventArgs> GameStarted;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            GameStarted?.Invoke(source, gameEventArgs);
        }

        public override void Add(Action<GameEventArgs> action)
        {
            GameStarted += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
