﻿using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;

namespace CardGame_Game.GameEvents
{
    public class PlayerInitializedEvent : GameEvent
    {
        public override string Name => "PlayerInitialized";

        public event EventHandler<GameEventArgs> PlayerInitialized;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            PlayerInitialized?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            PlayerInitialized += new EventHandler<GameEventArgs>((s, a) => action(a));
        }
    }
}
