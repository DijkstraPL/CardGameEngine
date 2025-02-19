﻿using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;

namespace CardGame_Game.GameEvents
{
    public class TurnFinishedEvent : GameEvent
    {
        public override string Name => "TurnFinished";

        public event EventHandler<GameEventArgs> TurnFinished;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            TurnFinished?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            TurnFinished += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
