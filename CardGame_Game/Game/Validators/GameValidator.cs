using CardGame_Game.Game.Interfaces;
using System;

namespace CardGame_Game.Game.Validators
{
    public abstract class GameValidator : IGameValidator
    {
        protected readonly IGame _game;

        public GameValidator(IGame game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public abstract bool IsValid();
    }
}
