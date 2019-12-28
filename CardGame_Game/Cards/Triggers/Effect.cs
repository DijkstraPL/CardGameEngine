using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Game.Cards.Triggers
{
    public class Effect : IEffect
    {
        private readonly Action<IGame> _action;

        public Effect(Action<IGame> action)
        {
            _action = action;
        }

        public void Invoke(IGame game)
        {
            _action(game);
        }
    }
}
