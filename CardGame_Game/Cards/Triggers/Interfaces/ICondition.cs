using CardGame_Game.Game.Interfaces;

namespace CardGame_Game.Cards.Triggers.Interfaces
{
    public interface ICondition
    {
        bool Validate(IGame game);
    }
}
