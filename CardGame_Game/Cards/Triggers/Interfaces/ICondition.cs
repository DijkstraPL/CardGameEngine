using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;

namespace CardGame_Game.Cards.Triggers.Interfaces
{
    public interface ICondition
    {
        string Name { get; }
        bool Validate(GameEventArgs gameEventArgs, params string[] args);
    }
}
