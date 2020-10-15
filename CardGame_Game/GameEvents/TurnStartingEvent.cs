using CardGame_Game.Cards;
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
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            TurnStarting += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
