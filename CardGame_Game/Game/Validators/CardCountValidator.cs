using CardGame_Game.Game.Interfaces;

namespace CardGame_Game.Game.Validators
{
    public class CardCountValidator : GameValidator
    {
        public const int LandCardCount = 15;
        public const int CardCount = 40;

        public CardCountValidator(IGame game) : base(game)
        {
        }

        public override bool IsValid() 
            => IsLandDeckCorrect() && IsNormalDeckCorrect();
        private bool IsLandDeckCorrect() 
            => _game.CurrentPlayer.LandDeck.Count == LandCardCount &&
                _game.NextPlayer.LandDeck.Count == LandCardCount;

        private bool IsNormalDeckCorrect() 
            => _game.CurrentPlayer.Deck.Count >= CardCount &&
                _game.NextPlayer.Deck.Count >= CardCount;
    }
}
