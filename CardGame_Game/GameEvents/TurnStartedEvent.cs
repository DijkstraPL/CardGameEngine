using CardGame_Game.Cards;
using CardGame_Game.Game;
using CardGame_Game.Rules.When;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class TurnStartedEvent : GameEvent
    {
        public override string Name => "TurnStarted";

        public event EventHandler<GameEventArgs> TurnStarted;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            TurnStarted?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            TurnStarted += new EventHandler<GameEventArgs>((s,a) => action(a));
        }
    }
}
