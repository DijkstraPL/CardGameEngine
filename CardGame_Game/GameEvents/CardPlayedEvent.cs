using CardGame_Game.Cards;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.GameEvents
{
    public class CardPlayedEvent : GameEvent
    {
        public override string Name => "CardPlayed";

        public event EventHandler<GameEventArgs> CardPlayed;

        public override void Raise(object source, GameEventArgs gameEventArgs)
        {
            CardPlayed?.Invoke(source, gameEventArgs);
            base.Raise(source, gameEventArgs);
        }

        public override void Add(GameCard sourceCard, Action<GameEventArgs> action)
        {
            CardPlayed += new EventHandler<GameEventArgs>((s, a) =>
            {
                    action(a);
            });
        }
    }
}
