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
    [Export(nameof(Chapel), typeof(IRule))]
    public class Chapel : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gameCard.CardState == CardState.OnField &&
                      gameCard.Owner == gea.Player &&
                      gea.SourceCard == gameCard &&
                    Int32.TryParse(args[0], out int value))
                {
                    foreach (var card in gameCard.Owner.AllCards)
                    {
                        if (card is IHealthy healthy && card.Kind == Kind.Creature)
                            healthy.AddHealthCalculation((card => gameCard.CardState == CardState.OnField, value));
                    }
                }
            });
        }
    }
}
