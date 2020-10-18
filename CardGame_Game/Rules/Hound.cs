using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;
using System.Linq;

namespace CardGame_Game.Rules
{
    [Export(nameof(Hound), typeof(IRule))]
    public class Hound : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            bool attackAdded = false;

            gameEventsContainer.UnitBeingAttackingEvent.Add(gameCard, gea =>
            {
                var target = gea.Targets.FirstOrDefault();
                if (gameCard == target &&
                    gea.SourceCard != null &&
                    target is IAttacker attacker &&
                    gea.SourceCard.Kind == Kind.Creature &&
                    Int32.TryParse(args[0], out int value))
                {
                    attacker.AttackCalculators.Add((card => true, value));
                    attackAdded = true;
                }
            });

            gameEventsContainer.UnitAttackedEvent.Add(gameCard, gea =>
            {
                var target = gea.Targets?.FirstOrDefault();
                if (gea.SourceCard == gameCard &&
                    target != null &&
                    target is IHealthy healthy &&
                    target.Kind == Kind.Creature &&
                    attackAdded &&
                    Int32.TryParse(args[0], out int value))
                {
                    healthy.AddHealthCalculation((card => true, -value));
                    attackAdded = false;
                }
            });
        }
    }
}
