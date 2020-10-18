using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;
using System.Linq;

namespace CardGame_Game.Rules
{
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
}
