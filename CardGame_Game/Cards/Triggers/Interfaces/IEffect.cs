using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;

namespace CardGame_Game.Cards.Triggers.Interfaces
{
    public interface IEffect
    {
        string Name { get; }
        void Invoke(GameEventArgs gameEventArgs, params string[] args);
    }
}
