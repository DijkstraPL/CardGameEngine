using CardGame_Data.Data.Enums;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Rules;
using NUnit.Framework;

namespace CardGame_GameTests.Rules
{
    [TestFixture]
    public class WatchTowerTests : BaseCardRuleTests
    {
        [Test]
        public void CanPlayAnotherLandCardAfterPlayingThis()
        {
            var watchTower = new WatchTower();

            InitPlayer();
            _player.SetupProperty(p => p.IsLandCardPlayed, true);
            InitSourceCard();
            _sourceCard.As<ICooldown>();
            _sourceCard.Object.CardState = CardState.OnField;

            InitGame();
            InitEvents();
            InitGameEventArgs();

            watchTower.Init(_sourceCard.Object, _gameEventsContainer.Object, new string[] { "1" });

            _cardPlayedEvent.Raise(null, _gameEventArgs);

            Assert.That(_player.Object.IsLandCardPlayed, Is.EqualTo(false));
        }

        [Test]
        public void IncreaseEnergyWhenTurnStarted()
        {
            var watchTower = new WatchTower();

            InitPlayer();
            _player.SetupProperty(p => p.IsLandCardPlayed, true);

            InitSourceCard();
            _sourceCard.As<ICooldown>();
            _sourceCard.As<ICooldown>().Setup(c => c.Cooldown).Returns(0);
            _sourceCard.Object.CardState = CardState.OnField;

            InitGame();
            InitEvents();
            InitGameEventArgs();

            watchTower.Init(_sourceCard.Object, _gameEventsContainer.Object, new string[] { "1" });

            _turnStartedEvent.Raise(null, _gameEventArgs);

            _player.Verify(p => p.IncreaseEnergy(CardColor.Blue, 1));
        }
    }
}
