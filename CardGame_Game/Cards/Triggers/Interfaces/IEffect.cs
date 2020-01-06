using CardGame_Game.Game.Interfaces;

namespace CardGame_Game.Cards.Triggers.Interfaces
{
    public interface IEffect
    {
        void Invoke(params string[] args);
    }
}
