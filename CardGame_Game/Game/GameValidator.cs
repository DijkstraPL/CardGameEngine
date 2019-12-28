using CardGame_Game.Game.Interfaces;
using System;

namespace CardGame_Game.Game
{
    public class GameValidator : IGameValidator
    {
        public const int LandCardCount = 15;
        public const int CardCount = 40;

        private readonly IGame _game;

        public GameValidator(IGame game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public bool IsValid()
        {
            return IsLandDeckCorrect() && IsNormalDeckCorrect();
        }

        private bool IsLandDeckCorrect()
        {
            return _game.FirstPlayer.LandDeck.Count == LandCardCount &&
                _game.SecondPlayer.LandDeck.Count == LandCardCount;
        }

        private bool IsNormalDeckCorrect()
        {
            return _game.FirstPlayer.Deck.Count >= CardCount &&
                _game.SecondPlayer.Deck.Count >= CardCount;
        }
    }
}
