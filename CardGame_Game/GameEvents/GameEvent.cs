using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;

namespace CardGame_Game.GameEvents
{
    public abstract class GameEvent<T> : GameEvent where T: EventArgs 
    {
        public abstract void Raise(object source, T gameEventArgs);
        public abstract void Add(Action<T> action);
    }

    public abstract class GameEvent
    {
        public abstract string Name { get; }

        public abstract void Add(GameCard sourceCard, Action<GameEventArgs> action);
        public abstract void Raise(object source, GameEventArgs gameEventArgs);
    }
}