using CardGame_Game.Cards;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules
{
    [Export(nameof(MoraleBoost), typeof(IRule))]
    public class MoraleBoost : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.SpellCastingEvent.Add(gameCard, gea =>
            {
                if (gea.SourceCard == gameCard && 
                Int32.TryParse(args[0], out int value) &&
                gameCard.Owner is BluePlayer bluePlayer)
                {
                    bluePlayer.Morale += value;
                }
            });
        }
    }
}
