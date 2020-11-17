using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class GameFinishedEvent : GameEvent
    {
        public override string Name => "GameFinished";

        public event EventHandler<GameEventArgs> GameFinished;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            GameFinished?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            GameFinished += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
