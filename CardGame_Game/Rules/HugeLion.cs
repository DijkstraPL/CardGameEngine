using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules
{
    [Export(nameof(HugeLion), typeof(IRule))]
    public class HugeLion : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.TurnStartedEvent.Add(gameCard, gea =>
            {
                var enemyPlayer = gea.Game.NextPlayer;
                if (gameCard.Owner == gea.Player &&
                    gameCard.CardState == CardState.OnField &&
                    gameCard is IAttacker attacker &&
                    Int32.TryParse(args[0], out int life) &&
                    Int32.TryParse(args[1], out int energy) &&
                    enemyPlayer != gameCard.Owner)
                {
                    if (enemyPlayer.FinalHealth < life)
                        gameCard.Owner.IncreaseEnergy(CardColor.Blue, energy);
                }
            });
        }
    }
}
