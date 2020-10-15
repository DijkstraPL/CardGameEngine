using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class UnitBeingAttackingEvent : GameEvent
    {
        public override string Name => "UnitBeingAttacking";

        public event EventHandler<GameEventArgs> UnitBeingAttacking;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            UnitBeingAttacking?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            UnitBeingAttacking += new EventHandler<GameEventArgs>((s, a) =>
            {
                if (sourceCard == a.SourceCard)
                    action(a);
            });
        }
    }
}

