using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;
using System.Linq;
using CardGame_Game.Players.Interfaces;

namespace CardGame_Game.Rules
{
    [Export(nameof(Spearman), typeof(IRule))]
    public class Spearman : IRule
    {
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
                    healthy.AddHealthCalculation((card =>
                    {
                        if (gameCard.Owner is IBluePlayer bluePlayer)
                            return bluePlayer.Morale >= morale;
                        return false;
                    }, value));
            });

            gameEventsContainer.UnitBeingAttackingEvent.Add(gameCard, gea =>
            {
                var target = gea.Targets.FirstOrDefault();
                if (gameCard == gea.SourceCard &&
                    target != null &&
                    target is IHealthy healthy &&
                    !target.Trait.HasFlag(Trait.DistanceAttack) &&
                    Int32.TryParse(args[2], out int value))
                    healthy.AddHealthCalculation((card => true, -value));
            });
        }
    }
}
