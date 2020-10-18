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
    [Export(nameof(StrengthBlessing), typeof(IRule))]
    public class StrengthBlessing : IRule
    {
        private int _castTurn;

        public void Init(GameCard gameCard, IGameEventsContainer gameEventsContainer, string[] args)
        {
            if (gameEventsContainer == null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            gameEventsContainer.SpellCastingEvent.Add(gameCard, gea =>
            {
                const int turnsAmount = 1;

                var target = gea.Targets.FirstOrDefault();
                if (gea.SourceCard == gameCard && 
                    target != null &&
                    target.CardState == CardState.OnField &&
                    Int32.TryParse(args[0], out int value) &&
                    target is IAttacker attacker)
                {
                    _castTurn = gea.Game.TurnCounter;
                    attacker.AttackCalculators.Add((card => _castTurn + turnsAmount > gea.Game.TurnCounter, value));
                }
            });
        }
    }
}
