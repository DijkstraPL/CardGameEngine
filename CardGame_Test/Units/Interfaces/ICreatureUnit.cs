using CardGame_Test.BoardTable;

namespace CardGame_Test.Units.Interfaces
{
    public interface ICreatureUnit
    {
        int CurrentReactionTime { get; set; }

        void Attack(Field field);
    }
}
