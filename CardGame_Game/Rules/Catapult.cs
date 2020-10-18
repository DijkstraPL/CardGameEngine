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
}
