using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace CardGame_Game.Rules
{
    [Export(nameof(BasicBlueLand), typeof(IRule))]
    public class BasicBlueLand : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.TurnStartedEvent.Add(gameCard, gea =>
            {
                if (gea.Player == gameCard.Owner &&
                    gameCard.CardState == CardState.OnField &&
                    gameCard is ICooldown cooldown &&
                    cooldown.Cooldown == 0 &&
                    Int32.TryParse(args[0], out int amount))
                    gea.Player.IncreaseEnergy(CardColor.Blue, amount);
            });
        }
    }

    [Export(nameof(Villager), typeof(IRule))]
    public class Villager : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.PlayerInitializedEvent.Add(gameCard, gea =>
            {
                if (gameCard.Owner == gea.Player &&
                    gameCard is IAttacker attacker &&
                    Int32.TryParse(args[0], out int morale) &&
                    Int32.TryParse(args[1], out int value))
                    attacker.AttackCalculators.Add((card =>
                    {
                        if (gameCard.Owner is BluePlayer bluePlayer)
                            return bluePlayer.Morale >= morale;
                        return false;
                    }, value: value));
            });
        }
    }

    [Export(nameof(PriestOfTheDeadSun), typeof(IRule))]
    public class PriestOfTheDeadSun : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gameCard.CardState == CardState.OnField &&
                    gameCard.Owner == gea.Player &&
                    gea.SourceCard == gameCard)
                {
                    const int value = 1;
                    var field = gameCard.Owner.BoardSide.Fields.FirstOrDefault(f => f.Card == gameCard);
                    if (field != null)
                    {
                        var fields = gameCard.Owner.BoardSide.GetNeighbourFields(field);
                        fields.Where(f => f.Card != null)
                            .ToList()
                            .ForEach(f => f.Card.HealthCalculators.Add((card => true, value)));
                    }
                }
            });

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gameCard.CardState == CardState.OnField &&
                    gameCard.Owner == gea.Player &&
                    gea.SourceCard == gameCard)
                {
                    const int value = 2;
                    if (gameCard.Owner.FinalHealth + value > gameCard.Owner.BaseHealth)
                        gameCard.Owner.HealthCalculators.Add((card => true, gameCard.Owner.BaseHealth - gameCard.Owner.FinalHealth ?? 0));
                    else
                        gameCard.Owner.HealthCalculators.Add((card => true, value));
                }
            });
        }
    }

    [Export(nameof(HasteBlessing), typeof(IRule))]
    public class HasteBlessing : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.SpellCastingEvent.Add(gameCard, gea =>
            {
                const int value = 1;

                var target = gea.Targets.FirstOrDefault();
                if (target != null &&
                    target.CardState == CardState.OnField &&
                    target is ICooldown cooldown)
                {
                    if (cooldown.Cooldown - value >= 0)
                        cooldown.Cooldown -= value;
                    else
                        cooldown.Cooldown = 0;
                }
            });
        }
    }

    [Export(nameof(WatchTower), typeof(IRule))]
    public class WatchTower : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.TurnStartedEvent.Add(gameCard, gea =>
            {
                if (gea.Player == gameCard.Owner &&
                    gameCard.CardState == CardState.OnField &&
                    gameCard is ICooldown cooldown &&
                    cooldown.Cooldown == 0 &&
                    Int32.TryParse(args[0], out int amount))
                {
                    gea.Player.IncreaseEnergy(CardColor.Blue, amount);
                }
            });

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gameCard.CardState == CardState.OnField &&
                    gameCard.Owner == gea.Player &&
                    gea.SourceCard == gameCard)
                {
                    gameCard.Owner.IsLandCardPlayed = false;
                }
            });
        }
    }

    [Export(nameof(BlacksmithGuild), typeof(IRule))]
    public class BlacksmithGuild : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            const string name = "Blacksmith guild";

            bool effectUsed = false;

            gameEventsContainer.TurnStartingEvent.Add(gameCard, gea =>
            {
                if (gea.Player == gameCard.Owner)
                    effectUsed = false;
            });

            gameEventsContainer.TurnStartedEvent.Add(gameCard, gea =>
            {
                if (gea.Player == gameCard.Owner &&
                    gameCard.CardState == CardState.OnField &&
                    gameCard is ICooldown cooldown &&
                    cooldown.Cooldown == 0 &&
                    Int32.TryParse(args[0], out int amount))
                {
                    gea.Player.IncreaseEnergy(CardColor.Blue, amount);
                }
            });

            gameEventsContainer.TurnStartedEvent.Add(gameCard, gea =>
            {
                if (gea.Player == gameCard.Owner &&
                    gameCard.CardState == CardState.OnField &&
                    gameCard is ICooldown cooldown &&
                    cooldown.Cooldown == 0 &&
                    Int32.TryParse(args[0], out int amount) &&
                    gameCard.Owner.BoardSide.Fields.Count(f => f.Card?.Name == name) >= 2 &&
                    !effectUsed)
                {
                    gea.Player.IncreaseEnergy(CardColor.Blue, amount);
                    effectUsed = true;
                }
            });
        }
    }

    [Export(nameof(OldKnight), typeof(IRule))]
    public class OldKnight : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.UnitAttackedEvent.Add(gameCard, gea =>
            {
                if (gea.Player == gameCard.Owner &&
                    gameCard.CardState == CardState.OnField &&
                    Int32.TryParse(args[0], out int amount) &&
                    gameCard is ICooldown cooldown)
                {
                    cooldown.BaseCooldown += amount;
                }
            });
        }
    }

    [Export(nameof(StrengthBlessing), typeof(IRule))]
    public class StrengthBlessing : IRule
    {
        private int _castTurn;

        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.SpellCastingEvent.Add(gameCard, gea =>
            {
                const int turnsAmount = 1;

                var target = gea.Targets.FirstOrDefault();
                if (target != null &&
                    target.CardState == CardState.OnField &&
                    Int32.TryParse(args[0], out int value) &&
                    target is IAttacker attacker)
                {
                    _castTurn = gea.Game.TurnCounter;
                    attacker.AttackCalculators.Add((card => _castTurn + turnsAmount > gea.Game.TurnCounter, value));
                }
            });
        }
    }

    [Export(nameof(Spearman), typeof(IRule))]
    public class Spearman : IRule
    {
        private int _castTurn;

        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.PlayerInitializedEvent.Add(gameCard, gea =>
            {
                if (gameCard.Owner == gea.Player &&
                    gameCard is IHealthy healthy &&
                    Int32.TryParse(args[0], out int morale) &&
                    Int32.TryParse(args[1], out int value))
                    healthy.HealthCalculators.Add((card =>
                    {
                        if (gameCard.Owner is BluePlayer bluePlayer)
                            return bluePlayer.Morale >= morale;
                        return false;
                    }, value));
            });

            gameEventsContainer.UnitBeingAttackingEvent.Add(gameCard, gea =>
            {
                var target = gea.Targets.FirstOrDefault();
                if (target != null &&
                    target is IHealthy healthy &&
                    !target.Trait.HasFlag(Trait.DistanceAttack) &&
                    Int32.TryParse(args[2], out int value))
                    healthy.HealthCalculators.Add((card => true, -value));
            });
        }
    }

    [Export(nameof(Hound), typeof(IRule))]
    public class Hound : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.UnitAttackedEvent.Add(gameCard, gea =>
            {
                var target = gea.Targets?.FirstOrDefault();
                if (target != null &&
                    target is IHealthy healthy &&
                    target.Kind == Kind.Creature &&
                    Int32.TryParse(args[0], out int value))
                {
                    healthy.HealthCalculators.Add((card => true, -value));
                }
            });
        }
    }

    [Export(nameof(Chapel), typeof(IRule))]
    public class Chapel : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gameCard.CardState == CardState.OnField &&
                      gameCard.Owner == gea.Player &&
                      gea.SourceCard == gameCard &&
                    Int32.TryParse(args[0], out int value))
                {
                    foreach (var card in gameCard.Owner.AllCards)
                    {
                        if (card is IHealthy healthy && card.Kind == Kind.Creature)
                            healthy.HealthCalculators.Add((card => gameCard.CardState == CardState.OnField, value));
                    }
                }
            });
        }
    }

    [Export(nameof(HighPriestOfTheDeadSun), typeof(IRule))]
    public class HighPriestOfTheDeadSun : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gameCard.CardState == CardState.OnField &&
                      gameCard.Owner == gea.Player &&
                      gea.SourceCard == gameCard &&
                    Int32.TryParse(args[0], out int value))
                {
                    foreach (var card in gameCard.Owner.AllCards)
                    {
                        if (card is IAttacker healthy && card.Kind == Kind.Creature)
                            healthy.AttackCalculators.Add((card =>
                            {
                                var field = gameCard.Owner.BoardSide.Fields.FirstOrDefault(f => f.Card == gameCard);
                                var neighbourFields = gameCard.Owner.BoardSide.GetNeighbourFields(field);
                                return gameCard.CardState == CardState.OnField &&
                                neighbourFields.Any(nf => nf.Card == card);
                            }, value));
                    }
                }
            });
        }
    }

    [Export(nameof(Catapult), typeof(IRule))]
    public class Catapult : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gameCard.CardState == CardState.OnField &&
                      gameCard.Owner == gea.Player &&
                      gea.SourceCard == gameCard)
                {
                    var catapults = gameCard.Owner.BoardSide.Fields.Where(f => f.Card?.Name == gameCard.Name);
                    if (catapults.Count() > 0 && gameCard is ICooldown cooldown)
                        cooldown.Cooldown = Math.Min((int)cooldown.BaseCooldown, (int)catapults.Min(c => c.Card.Cooldown));
                }
            });
        }
    }

    [Export(nameof(DefenderOfComrades), typeof(IRule))]
    public class DefenderOfComrades : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.PlayerInitializedEvent.Add(gameCard, gea =>
            {
                if (gameCard.Owner == gea.Player &&
                      gameCard is IAttacker attacker &&
                    gameCard.Owner is BluePlayer bluePlayer)
                {
                    attacker.AttackFuncCalculators.Add((card => true, card => bluePlayer.Morale));
                }
            });
        }
    }

    [Export(nameof(HighestPriestOfTheDeadSun), typeof(IRule))]
    public class HighestPriestOfTheDeadSun : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.TurnStartedEvent.Add(gameCard, gea =>
            {
                if (gameCard.Owner == gea.Player &&
                    gameCard.CardState == CardState.OnField &&
                    gameCard is ICooldown cooldown &&
                    cooldown.Cooldown == 0 &&
                    Int32.TryParse(args[0], out int value))
                {
                    var field = gameCard.Owner.BoardSide.Fields.FirstOrDefault(f => f.Card == gameCard);
                    var neighbourFields = gameCard.Owner.BoardSide.GetNeighbourFields(field);

                    foreach (var neighbourField in neighbourFields)
                    {
                        if (neighbourField.Card?.Kind == Kind.Creature)
                            neighbourField.Card.Cooldown -= value;
                    }        
                }
            });
        }
    }

    [Export(nameof(MoraleBoost), typeof(IRule))]
    public class MoraleBoost : IRule
    {
        private int _castTurn;

        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.SpellCastingEvent.Add(gameCard, gea =>
            {
                if (Int32.TryParse(args[0], out int value) &&
                gameCard.Owner is BluePlayer bluePlayer)
                {
                    bluePlayer.Morale += value;
                }
            });
        }
    }

    [Export(nameof(Revocation), typeof(IRule))]
    public class Revocation : IRule
    {
        private int _castTurn;

        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.SpellCastingEvent.Add(gameCard, gea =>
            {
                var target = gea.Targets.FirstOrDefault();
                if (target != null &&
                target.Kind == Kind.Creature)
                {
                    gea.Game.SendCardToHand(target, target.Owner);
                }
            });
        }
    }
}
