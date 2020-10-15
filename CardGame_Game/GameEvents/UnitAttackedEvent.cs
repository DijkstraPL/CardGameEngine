using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class UnitAttackedEvent : GameEvent
    {
        public override string Name => "UnitAttacked";

        public event EventHandler<GameEventArgs> UnitAttacked;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            UnitAttacked?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            UnitAttacked += new EventHandler<GameEventArgs>((s, a) =>
            {
                if (sourceCard == a.SourceCard)
                    action(a);
            });
        }
    }
}
