using CardGame_Game.Cards;
using CardGame_Game.Cards.Enums;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;
using System.Linq;

namespace CardGame_Game.Rules
{
    [Export(nameof(PriestOfTheDeadSun), typeof(IRule))]
    public class PriestOfTheDeadSun : IRule
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
                    const int value = 1;
                    var field = gameCard.Owner.BoardSide.Fields.FirstOrDefault(f => f.Card == gameCard);
                    if (field != null)
                    {
                        var fields = gameCard.Owner.BoardSide.GetNeighbourFields(field);
                        fields.Where(f => f.Card != null)
                            .ToList()
                            .ForEach(f => f.Card.AddHealthCalculation((card => true, value)));
                    }
                }
            });

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gameCard.CardState == CardState.OnField &&
                    gameCard.Owner == gea.Player &&
                    gea.SourceCard == gameCard)
                {
                    const int value = 2;
                    if (gameCard.Owner.FinalHealth + value > gameCard.Owner.BaseHealth)
                        gameCard.Owner.AddHealthCalculation((card => true, gameCard.Owner.BaseHealth - gameCard.Owner.FinalHealth ?? 0));
                    else
                        gameCard.Owner.AddHealthCalculation((card => true, value));
                }
            });
        }
    }
}
