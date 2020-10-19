using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Players.Interfaces;
using CardGame_Game.Rules;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_GameTests.Rules
{
    [TestFixture]
    public class OldKnightTests
    {
        [Test]
        public void BaseCooldownShouldIncreaseAfterAttack()
        {
            var oldKnight = new OldKnight();

            var player = new Mock<IPlayer>();
            var card = new Card();

            var gameCard = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None);
            gameCard.As<ICooldown>();
            gameCard.As<ICooldown>().SetupProperty(c =>c.BaseCooldown, 1);

            gameCard.Object.CardState = CardState.OnField;

            var unitAttackedEvent = new UnitAttackedEvent();

            var gameEventsContainer = new Mock<IGameEventsContainer>();
            gameEventsContainer.Setup(ge => ge.UnitAttackedEvent).Returns(unitAttackedEvent);

            oldKnight.Init(gameCard.Object, gameEventsContainer.Object, new string[] { "1" });

            var gameEventsArgs = new GameEventArgs
            {
                Player = player.Object,
                SourceCard = gameCard.Object
            };

            unitAttackedEvent.Raise(null, gameEventsArgs);

            Assert.That(gameCard.As<ICooldown>().Object.BaseCooldown, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class WatchTowerTests
    {
        [Test]
        public void CanPlayAnotherLandCardAfterPlayingThis()
        {
            var watchTower = new WatchTower();

            var player = new Mock<IPlayer>();
            player.SetupProperty(p => p.IsLandCardPlayed, true);
            var card = new Card();

            var gameCard = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None);
            gameCard.As<ICooldown>();

            gameCard.Object.CardState = CardState.OnField;

            var cardPlayedEvent = new CardPlayedEvent();
            var turnStartedEvent = new TurnStartedEvent();

            var gameEventsContainer = new Mock<IGameEventsContainer>();
            gameEventsContainer.Setup(ge => ge.CardPlayedEvent).Returns(cardPlayedEvent);
            gameEventsContainer.Setup(ge => ge.TurnStartedEvent).Returns(turnStartedEvent);

            watchTower.Init(gameCard.Object, gameEventsContainer.Object, new string[] { "1" });

            var gameEventsArgs = new GameEventArgs
            {
                Player = player.Object,
                SourceCard = gameCard.Object
            };

            cardPlayedEvent.Raise(null, gameEventsArgs);

            Assert.That(player.Object.IsLandCardPlayed, Is.EqualTo(false));
        }

        [Test]
        public void IncreaseEnergyWhenTurnStarted()
        {
            var watchTower = new WatchTower();

            var player = new Mock<IPlayer>();
            player.SetupProperty(p => p.IsLandCardPlayed, true);
            var card = new Card();

            var gameCard = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None);
            gameCard.As<ICooldown>();
            gameCard.As<ICooldown>().Setup(c => c.Cooldown).Returns(0);

            gameCard.Object.CardState = CardState.OnField;

            var cardPlayedEvent = new CardPlayedEvent();
            var turnStartedEvent = new TurnStartedEvent();

            var gameEventsContainer = new Mock<IGameEventsContainer>();
            gameEventsContainer.Setup(ge => ge.CardPlayedEvent).Returns(cardPlayedEvent);
            gameEventsContainer.Setup(ge => ge.TurnStartedEvent).Returns(turnStartedEvent);

            watchTower.Init(gameCard.Object, gameEventsContainer.Object, new string[] { "1" });

            var gameEventsArgs = new GameEventArgs
            {
                Player = player.Object,
                SourceCard = gameCard.Object
            };

            turnStartedEvent.Raise(null, gameEventsArgs);

            player.Verify(p => p.IncreaseEnergy(CardColor.Blue, 1));
        }
    }

    [TestFixture]
    public class VillagerTests
    {
        [Test]
        public void ShouldIncreaseAttackWhenMoraleAreHigher()
        {
            var villager = new Villager();

            var player = new Mock<IPlayer>();
            player.As<IBluePlayer>();
            player.As<IBluePlayer>().Setup(bp => bp.Morale).Returns(2);
            var card = new Card();

            var gameCard = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None);
            var attacker = gameCard.As<IAttacker>();
            var attackCalculators = new List<(Func<IAttacker, bool> conditon, int value)>();
            gameCard.As<IAttacker>().Setup(c => c.AttackCalculators).Returns(attackCalculators);

            gameCard.Object.CardState = CardState.OnField;

            var playerInitializedEvent = new PlayerInitializedEvent();

            var gameEventsContainer = new Mock<IGameEventsContainer>();
            gameEventsContainer.Setup(ge => ge.PlayerInitializedEvent).Returns(playerInitializedEvent);

            villager.Init(gameCard.Object, gameEventsContainer.Object, new string[] { "1", "2" });

            var gameEventsArgs = new GameEventArgs
            {
                Player = player.Object,
            };

            playerInitializedEvent.Raise(null, gameEventsArgs);

            Assert.Multiple(() =>
            {
                Assert.That(attacker.Object.AttackCalculators.Count, Is.EqualTo(1));
                Assert.That(attacker.Object.AttackCalculators.First().conditon(attacker.Object), Is.EqualTo(true));
                Assert.That(attacker.Object.AttackCalculators.First().value, Is.EqualTo(2));
            });
        }
    }

    [TestFixture]
    public class StrengthBlessingTests
    {
        [Test]
        public void ShouldInccreaseTargetStrengthForOneTurn()
        {
            var strengthBlessing = new StrengthBlessing();

            var game = new Mock<IGame>();
            game.Setup(g => g.TurnCounter).Returns(2);

            var player = new Mock<IPlayer>();
            player.As<IBluePlayer>();
            var card = new Card();

            var gameCard = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None);
            var target = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None);
            var attacker = target.As<IAttacker>();
            var attackCalculators = new List<(Func<IAttacker, bool> conditon, int value)>();
            attacker.Setup(c => c.AttackCalculators).Returns(attackCalculators);

            target.Object.CardState = CardState.OnField;

            var spellCastingEvent = new SpellCastingEvent();

            var gameEventsContainer = new Mock<IGameEventsContainer>();
            gameEventsContainer.Setup(ge => ge.SpellCastingEvent).Returns(spellCastingEvent);

            strengthBlessing.Init(gameCard.Object, gameEventsContainer.Object, new string[] { "3" });

            var gameEventsArgs = new GameEventArgs
            {
                Player = player.Object,
                SourceCard = gameCard.Object,
                Targets = new List<GameCard> { target.Object },
                Game = game.Object

            };

            spellCastingEvent.Raise(null, gameEventsArgs);

            Assert.Multiple(() =>
            {
                Assert.That(attacker.Object.AttackCalculators.Count, Is.EqualTo(1));
                Assert.That(attacker.Object.AttackCalculators.First().conditon(attacker.Object), Is.EqualTo(true));
                Assert.That(attacker.Object.AttackCalculators.First().value, Is.EqualTo(3));
            });

            game.Setup(g => g.TurnCounter).Returns(3);

                Assert.That(attacker.Object.AttackCalculators.First().conditon(attacker.Object), Is.EqualTo(false));
        }
    }

    [TestFixture]
    public class SpearmanTests
    {
        [Test]
        public void ShouldIncreaseHealthWhenMoraleAreHigher()
        {
            var spearman = new Spearman();

            var player = new Mock<IPlayer>();
            player.As<IBluePlayer>();
            player.As<IBluePlayer>().Setup(bp => bp.Morale).Returns(2);
            var card = new Card();

            var gameCard = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None);
            var healthy = gameCard.As<IHealthy>();
            var healthCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
            healthy.Setup(c => c.HealthCalculators).Returns(healthCalculators);
            healthy.Setup(h => h.AddHealthCalculation(It.IsAny<(Func<IHealthy, bool> conditon, int value)>()))
                .Callback<(Func<IHealthy, bool> conditon, int value)>(c => healthCalculators.Add(c));

            gameCard.Object.CardState = CardState.OnField;

            var playerInitializedEvent = new PlayerInitializedEvent();
            var unitBeingAttackingEvent = new UnitBeingAttackingEvent();

            var gameEventsContainer = new Mock<IGameEventsContainer>();
            gameEventsContainer.Setup(ge => ge.PlayerInitializedEvent).Returns(playerInitializedEvent);
            gameEventsContainer.Setup(ge => ge.UnitBeingAttackingEvent).Returns(unitBeingAttackingEvent);

            spearman.Init(gameCard.Object, gameEventsContainer.Object, new string[] { "1", "2", "3" });

            var gameEventsArgs = new GameEventArgs
            {
                Player = player.Object,
            };

            playerInitializedEvent.Raise(null, gameEventsArgs);

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

            var player = new Mock<IPlayer>();
            player.As<IBluePlayer>();
            player.As<IBluePlayer>().Setup(bp => bp.Morale).Returns(2);
            var card = new Card();

            var gameCard = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None); 
            var target = new Mock<GameCard>(player.Object, card, "a", "b", 1, InvocationTarget.None);
            var healthy = target.As<IHealthy>();
            var healthyCalculators = new List<(Func<IHealthy, bool> conditon, int value)>();
            healthy.Setup(c => c.HealthCalculators).Returns(healthyCalculators);
            healthy.Setup(h => h.AddHealthCalculation(It.IsAny<(Func<IHealthy, bool> conditon, int value)>()))
                .Callback<(Func<IHealthy, bool> conditon, int value)>(c => healthcalculators.Add(c));

            gameCard.Object.CardState = CardState.OnField;

            var playerInitializedEvent = new PlayerInitializedEvent();
            var unitBeingAttackingEvent = new UnitBeingAttackingEvent();

            var gameEventsContainer = new Mock<IGameEventsContainer>();
            gameEventsContainer.Setup(ge => ge.PlayerInitializedEvent).Returns(playerInitializedEvent);
            gameEventsContainer.Setup(ge => ge.UnitBeingAttackingEvent).Returns(unitBeingAttackingEvent);

            spearman.Init(gameCard.Object, gameEventsContainer.Object, new string[] { "1", "2", "3" });

            var gameEventsArgs = new GameEventArgs
            {
                Player = player.Object,
                SourceCard = gameCard.Object,
                Targets = new List<GameCard> { }
            };

            unitBeingAttackingEvent.Raise(null, gameEventsArgs);

            Assert.Multiple(() =>
            {
                Assert.That(healthy.Object.HealthCalculators.Count, Is.EqualTo(1));
                Assert.That(healthy.Object.HealthCalculators.First().conditon(healthy.Object), Is.EqualTo(true));
                Assert.That(healthy.Object.HealthCalculators.First().value, Is.EqualTo(2));
            });
        }
    }
}
