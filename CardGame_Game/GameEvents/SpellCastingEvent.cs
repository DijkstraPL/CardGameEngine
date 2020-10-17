using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class SpellCastingEvent : GameEvent
    {
        public override string Name => "SpellCasting";

        public event EventHandler<GameEventArgs> SpellCasting;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            SpellCasting?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            SpellCasting += new EventHandler<GameEventArgs>((s, a) =>
            {
                    action(a);
            });
        }
    }
}
