using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules
{
    [Export(nameof(OldKnight), typeof(IRule))]
    public class OldKnight : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.UnitAttackedEvent.Add(gameCard, gea =>
            {
                if (gea.SourceCard == gameCard &&
                    gea.Player == gameCard.Owner &&
                    gameCard.CardState == CardState.OnField &&
                    Int32.TryParse(args[0], out int amount) &&
                    gameCard is ICooldown cooldown)
                {
                    cooldown.BaseCooldown += amount;
                }
            });
        }
    }
}
