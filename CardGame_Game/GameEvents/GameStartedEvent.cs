using CardGame_Game.Cards;
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
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            GameStarted += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
