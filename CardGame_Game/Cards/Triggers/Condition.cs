using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game.Interfaces;
using System;

namespace CardGame_Game.Cards.Triggers
{
    public class Condition : ICondition
    {
        private readonly Predicate<IGame> _validator;

        public Condition(Predicate<IGame> validator)
        {
            _validator = validator;
        }

        public bool Validate(IGame game)
        {
            return _validator(game);
        }
    }
}
