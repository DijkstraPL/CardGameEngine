﻿using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules
{
    [Export(nameof(FireMonk), typeof(IRule))]
    public class FireMonk : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.PlayerInitializedEvent.Add(gameCard, gea =>
            {
                if (gameCard.Owner == gea.Player &&
                      gameCard is IAttacker attacker &&
                    gameCard.Owner is BluePlayer bluePlayer)
                {
                    attacker.AttackFuncCalculators.Add((card => true, card => bluePlayer.Morale));
                }
            });

            gameEventsContainer.CardPlayedEvent.Add(gameCard, gea =>
            {
                if (gea.SourceCard == gameCard &&
                    gameCard.Owner is BluePlayer bluePlayer &&
                    int.TryParse(args[0], out int value))
                    bluePlayer.Morale += value;
            });
        }
    }
}
