using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Players.Interfaces;
using CardGame_Game.Rules;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_GameTests.Rules
{
    [TestFixture]
    public class VillagerTests : BaseCardRuleTests
    {
        [Test]
        public void ShouldIncreaseAttackWhenMoraleAreHigher()
        {
            var villager = new Villager();

            InitPlayer();
            _player.As<IBluePlayer>();
            _player.As<IBluePlayer>().Setup(bp => bp.Morale).Returns(2);

            InitSourceCard();
            var attacker = _sourceCard.As<IAttacker>();
            var attackCalculators = new List<(Func<IAttacker, bool> conditon, int value)>();
            attacker.Setup(c => c.AttackCalculators).Returns(attackCalculators);
            _sourceCard.Object.CardState = CardState.OnField;

            InitGame();
            InitEvents();
            InitGameEventArgs();

            villager.Init(_sourceCard.Object, _gameEventsContainer.Object, new string[] { "1", "2" });

            _playerInitializedEvent.Raise(null, _gameEventArgs);

            Assert.Multiple(() =>
            {
                Assert.That(attacker.Object.AttackCalculators.Count, Is.EqualTo(1));
                Assert.That(attacker.Object.AttackCalculators.First().conditon(attacker.Object), Is.EqualTo(true));
                Assert.That(attacker.Object.AttackCalculators.First().value, Is.EqualTo(2));
            });
        }
    }
}
