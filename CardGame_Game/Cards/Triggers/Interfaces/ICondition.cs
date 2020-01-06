using CardGame_Game.Game.Interfaces;

namespace CardGame_Game.Cards.Triggers.Interfaces
{
    public interface ICondition
    {
        string Happend { get; }
        bool Validate(params string[] args);
    }
}
