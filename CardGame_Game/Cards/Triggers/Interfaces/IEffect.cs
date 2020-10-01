using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace CardGame_Game.Cards.Triggers.Interfaces
{
    public interface IEffect
    {
        string Name { get; }
        void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args);
    }
}
