using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;

namespace CardGame_Game.GameEvents
{
    public class GameStartingEvent : GameEvent
    {
        public override string Name => "GameStarting";

        public event EventHandler<GameEventArgs> GameStarting;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            GameStarting?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            GameStarting += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
