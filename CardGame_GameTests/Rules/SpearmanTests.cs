using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Players.Interfaces;
using CardGame_Game.Rules;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_GameTests.Rules
{
    [TestFixture]
    public class SpearmanTests : BaseCardRuleTests
    {
        [Test]
        public void ShouldIncreaseHealthWhenMoraleAreHigher()
        {
            var spearman = new Spearman();

            InitPlayer();
            _player.As<IBluePlayer>();
            _player.As<IBluePlayer>().Setup(bp => bp.Morale).Returns(2);

            InitSourceCard();
            var healthy = _sourceCard.As<IHealthy>();
            var healthCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
            healthy.Setup(c => c.HealthCalculators).Returns(healthCalculators);
            healthy.Setup(h => h.AddHealthCalculation(It.IsAny<(Func<IHealthy, bool> conditon, int value)>()))
                .Callback<(Func<IHealthy, bool> conditon, int value)>(c => healthCalculators.Add(c));
            _sourceCard.Object.CardState = CardState.OnField;

            InitGame();
            InitEvents();
            InitGameEventArgs();

            spearman.Init(_sourceCard.Object, _gameEventsContainer.Object, new string[] { "1", "2", "3" });

            _playerInitializedEvent.Raise(null, _gameEventArgs);

            Assert.Multiple(() =>
            {
                Assert.That(healthy.Object.HealthCalculators.Count, Is.EqualTo(1));
                Assert.That(healthy.Object.HealthCalculators.First().conditon(healthy.Object), Is.EqualTo(true));
                Assert.That(healthy.Object.HealthCalculators.First().value, Is.EqualTo(2));
            });
        }


        [Test]
        public void ShouldHurtEnemyBeforeAttack()
        {
            var spearman = new Spearman();

            InitPlayer();
            _player.As<IBluePlayer>();
            _player.As<IBluePlayer>().Setup(bp => bp.Morale).Returns(2);

            InitSourceCard();
            _sourceCard.Object.CardState = CardState.OnField;

            var card = new Card();
            var target = new Mock<GameCard>(_player.Object, card, "a", "b", 1, InvocationTarget.None);
            var healthy = target.As<IHealthy>();
            var healthyCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
            healthy.Setup(c => c.HealthCalculators).Returns(healthyCalculators);
            healthy.Setup(h => h.AddHealthCalculation(It.IsAny<(Func<IHealthy, bool> conditon, int value)>()))
                .Callback<(Func<IHealthy, bool> conditon, int value)>(c => healthyCalculators.Add(c));

            InitGame();
            InitEvents();
            InitGameEventArgs();
            _gameEventArgs.Targets = new List<GameCard> { target.Object };

            spearman.Init(_sourceCard.Object, _gameEventsContainer.Object, new string[] { "1", "2", "3" });

            _unitBeingAttackingEvent.Raise(null, _gameEventArgs);

            Assert.Multiple(() =>
            {
                Assert.That(healthy.Object.HealthCalculators.Count, Is.EqualTo(1));
                Assert.That(healthy.Object.HealthCalculators.First().conditon(healthy.Object), Is.EqualTo(true));
                Assert.That(healthy.Object.HealthCalculators.First().value, Is.EqualTo(-3));
            });
        }
    }
}
