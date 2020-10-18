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
}
