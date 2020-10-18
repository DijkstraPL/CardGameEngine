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
    [Export(nameof(HasteBlessing), typeof(IRule))]
    public class HasteBlessing : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.SpellCastingEvent.Add(gameCard, gea =>
            {
                const int value = 1;

                var target = gea.Targets.FirstOrDefault();
                if (gea.SourceCard == gameCard && 
                    target != null &&
                    target.CardState == CardState.OnField &&
                    target is ICooldown cooldown)
                {
                    if (cooldown.Cooldown - value >= 0)
                        cooldown.Cooldown -= value;
                    else
                        cooldown.Cooldown = 0;
                }
            });
        }
    }
}
