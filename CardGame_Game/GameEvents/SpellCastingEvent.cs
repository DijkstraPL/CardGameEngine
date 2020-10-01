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
        }

        public override void Add(Action<GameEventArgs> action)
        {
            SpellCasting += new EventHandler<GameEventArgs>((s, a) =>
            {
                if (s == a.SourceCard)
                    action(a);
            });
        }
    }
}
