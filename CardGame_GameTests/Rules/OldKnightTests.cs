using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Players;
using CardGame_Game.Rules;
using Moq;
using Moq.Language;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_GameTests.Rules
{

    [TestFixture]
    public class OldKnightTests : BaseCardRuleTests
    {
        [Test]
        public void BaseCooldownShouldIncreaseAfterAttack()
        {
            var oldKnight = new OldKnight();

            InitPlayer();
            InitSourceCard();
            _sourceCard.As<ICooldown>();
            _sourceCard.As<ICooldown>().SetupProperty(c => c.BaseCooldown, 1);
            _sourceCard.Object.CardState = CardState.OnField;

            InitGame();
            InitEvents();
            InitGameEventArgs();

            oldKnight.Init(_sourceCard.Object, _gameEventsContainer.Object, new string[] { "1" });

            _unitAttackedEvent.Raise(null, _gameEventArgs);

            Assert.That(_sourceCard.As<ICooldown>().Object.BaseCooldown, Is.EqualTo(2));
        }
    }


    [TestFixture]
    public class PriestOfTheDeadSunTests : BaseCardRuleTests
    {
        [Test]
        public void IncreaseHealthOfNeighbourUnits()
        {
            var priestOfTheDeadSun = new PriestOfTheDeadSun();

            InitPlayer();
            var field1 = new Field(0, 0);
            var field2 = new Field(0, 1);
            var field3 = new Field(0, 2);
            _player.Setup(p => p.BoardSide.Fields).Returns(new List<Field> { field1, field2, field3 });
            _player.Setup(p => p.BoardSide.GetNeighbourFields(field1)).Returns(new List<Field> { field2 });
            _player.Setup(p => p.FinalHealth).Returns( 10);
            _player.Setup(p => p.BaseHealth).Returns(20);
            var playerHealthCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
            _player.Setup(c => c.HealthCalculators).Returns(playerHealthCalculators);
            _player.Setup(h => h.AddHealthCalculation(It.IsAny<(Func<IHealthy, bool> conditon, int value)>()))
                .Callback<(Func<IHealthy, bool> conditon, int value)>(c => playerHealthCalculators.Add(c));

            var sourceCard = new Mock<GameUnitCard>(_player.Object, new Card(), "a", "desc", 1, InvocationTarget.OwnEmptyField, 1, 2, 3);
            sourceCard.Object.CardState = CardState.OnField;
            field1.Card = sourceCard.Object;

            var neighbour = new Mock<GameUnitCard>(_player.Object, new Card(), "b", "desc", 1, InvocationTarget.OwnEmptyField, 1, 2, 3);
            var healthy = neighbour.As<IHealthy>();
            var healthCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
            healthy.Setup(c => c.HealthCalculators).Returns(healthCalculators);
            healthy.Setup(h => h.AddHealthCalculation(It.IsAny<(Func<IHealthy, bool> conditon, int value)>()))
                .Callback<(Func<IHealthy, bool> conditon, int value)>(c => healthCalculators.Add(c));
            field2.Card = neighbour.Object;

            InitGame();
            InitEvents();
            _gameEventArgs = new GameEventArgs
            {
                SourceCard = sourceCard.Object,
                Player = _player.Object,
                Game = _game.Object
            };

            priestOfTheDeadSun.Init(sourceCard.Object, _gameEventsContainer.Object, new string[0]);

            _cardPlayedEvent.Raise(null, _gameEventArgs);

            Assert.Multiple(() =>
            {
                Assert.That(field2.Card.HealthCalculators.Count(), Is.EqualTo(1));
                Assert.That(field2.Card.HealthCalculators.First().value, Is.EqualTo(1));
            });
        }

        [Test]
        public void IncreaseHealthOfPlayer()
        {
            var priestOfTheDeadSun = new PriestOfTheDeadSun();

            InitPlayer();
            var field1 = new Field(0, 0);
            var field2 = new Field(0, 1);
            var field3 = new Field(0, 2);
            _player.Setup(p => p.BoardSide.Fields).Returns(new List<Field> { field1, field2, field3 });
            _player.Setup(p => p.BoardSide.GetNeighbourFields(field1)).Returns(new List<Field> { field2 });
            _player.Setup(p => p.FinalHealth).Returns(10);
            _player.Setup(p => p.BaseHealth).Returns(20);
            var playerHealthCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
            _player.Setup(c => c.HealthCalculators).Returns(playerHealthCalculators);
            _player.Setup(h => h.AddHealthCalculation(It.IsAny<(Func<IHealthy, bool> conditon, int value)>()))
                .Callback<(Func<IHealthy, bool> conditon, int value)>(c => playerHealthCalculators.Add(c));

            var sourceCard = new Mock<GameUnitCard>(_player.Object, new Card(), "a", "desc", 1, InvocationTarget.OwnEmptyField, 1, 2, 3);
            sourceCard.Object.CardState = CardState.OnField;
            field1.Card = sourceCard.Object;

            var neighbour = new Mock<GameUnitCard>(_player.Object, new Card(), "b", "desc", 1, InvocationTarget.OwnEmptyField, 1, 2, 3);
            var healthy = neighbour.As<IHealthy>();
            var healthCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
            healthy.Setup(c => c.HealthCalculators).Returns(healthCalculators);
            healthy.Setup(h => h.AddHealthCalculation(It.IsAny<(Func<IHealthy, bool> conditon, int value)>()))
                .Callback<(Func<IHealthy, bool> conditon, int value)>(c => healthCalculators.Add(c));
            field2.Card = neighbour.Object;

            InitGame();
            InitEvents();
            _gameEventArgs = new GameEventArgs
            {
                SourceCard = sourceCard.Object,
                Player = _player.Object,
                Game = _game.Object
            };

            priestOfTheDeadSun.Init(sourceCard.Object, _gameEventsContainer.Object, new string[0]);

            _cardPlayedEvent.Raise(null, _gameEventArgs);

            Assert.Multiple(() =>
            {
                Assert.That(_player.Object.HealthCalculators.Count(), Is.EqualTo(1));
                Assert.That(_player.Object.HealthCalculators.First().value, Is.EqualTo(2));
            });
        }
    }
}
