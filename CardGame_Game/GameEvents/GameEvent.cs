using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;

namespace CardGame_Game.GameEvents
{
    public abstract class GameEvent
    {
        public abstract string Name { get; }

        public static event EventHandler<GameEventArgs> EventRaised;
        
        public abstract void Add(GameCard sourceCard, Action<GameEventArgs> action);
        public virtual void Raise(object source, GameEventArgs gameEventArgs)
        {
            EventRaised?.Invoke(this, gameEventArgs);
        }
    }
}