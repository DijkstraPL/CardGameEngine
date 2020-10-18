using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Composition;

namespace CardGame_Game.Rules
{
    [Export(nameof(Guard), typeof(IRule))]
    public class Guard : IRule
    {
        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.PlayerInitializedEvent.Add(gameCard, gea =>
            {
                if (gameCard.Owner == gea.Player &&
                    gameCard is IAttacker attacker &&
                    Int32.TryParse(args[0], out int morale) &&
                    Int32.TryParse(args[1], out int value))
                    attacker.AttackCalculators.Add((card =>
                    {
                        if (gameCard.Owner is BluePlayer bluePlayer)
                            return bluePlayer.Morale >= morale;
                        return false;
                    }, value));
            });
        }
    }
}
