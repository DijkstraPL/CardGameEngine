using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules
{
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
}
