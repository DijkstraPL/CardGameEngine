﻿using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;
using System.Linq;

namespace CardGame_Game.Rules
{
    [Export(nameof(Revocation), typeof(IRule))]
    public class Revocation : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.SpellCastingEvent.Add(gameCard, gea =>
            {
                var target = gea.Targets.FirstOrDefault();
                if (gea.SourceCard == gameCard &&  
                target != null &&                
                target.Kind == Kind.Creature)
                {
                    gea.Game.SendCardToHand(target, target.Owner);
                }
            });
        }
    }
}
