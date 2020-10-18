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
}
