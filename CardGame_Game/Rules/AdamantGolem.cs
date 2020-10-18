using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules
{
    [Export(nameof(AdamantGolem), typeof(IRule))]
    public class AdamantGolem : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.PlayerInitializedEvent.Add(gameCard, gea =>
            {
                if (gameCard.Owner == gea.Player &&
                      gameCard is IAttacker attacker &&
                      gameCard is IHealthy healthy)
                {
                    attacker.AttackFuncCalculators.Add((card => true, card => (int)healthy.FinalHealth));
                }
            });
        }
    }
}
