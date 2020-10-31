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
    public class StrengthBlessingTests : BaseCardRuleTests
    {
        [Test]
        public void ShouldInccreaseTargetStrengthForOneTurn()
        {
            var strengthBlessing = new StrengthBlessing();

            InitPlayer();
            _player.As<IBluePlayer>();

            InitSourceCard();

            var card = new Card();
            var target = new Mock<GameCard>(_player.Object, card, "a", "b", 1, InvocationTarget.None);
            var attacker = target.As<IAttacker>();
            var attackCalculators = new List<(Func<IAttacker, bool> conditon, int value)>();
            attacker.Setup(c => c.AttackCalculators).Returns(attackCalculators);
            target.Object.CardState = CardState.OnField;

            InitGame();
            _game.Setup(g => g.TurnCounter).Returns(2);
            InitEvents();
            InitGameEventArgs();

            strengthBlessing.Init(_sourceCard.Object, _gameEventsContainer.Object, new string[] { "3" });

            _gameEventArgs.Targets = new List<GameCard> { target.Object };

            _spellCastingEvent.Raise(null, _gameEventArgs);

            Assert.Multiple(() =>
            {
                Assert.That(attacker.Object.AttackCalculators.Count, Is.EqualTo(1));
                Assert.That(attacker.Object.AttackCalculators.First().conditon(attacker.Object), Is.EqualTo(true));
                Assert.That(attacker.Object.AttackCalculators.First().value, Is.EqualTo(3));
            });

            _game.Setup(g => g.TurnCounter).Returns(3);

            Assert.That(attacker.Object.AttackCalculators.First().conditon(attacker.Object), Is.EqualTo(false));
        }
    }
}
