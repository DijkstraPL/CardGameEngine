using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace CardGame_Game.Rules.Effects
{
    /// <summary>
    /// HighPriestOfTheDeadSun({value})
    /// </summary>
    [Export(Name, typeof(IEffect))]
    public class HighPriestOfTheDeadSun : IEffect
    {
        public const string Name = "HighPriestOfTheDeadSun";
        string IEffect.Name => Name;

        [ImportingConstructor]
        public HighPriestOfTheDeadSun()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args)
        {
            Func<IAttacker, bool> isNeighbour = (card) =>
            {
                var sourceField = gameEventArgs.Player.BoardSide.Fields.FirstOrDefault(f => f.Card == gameEventArgs.SourceCard);
                var consideredField = gameEventArgs.Player.BoardSide.Fields.FirstOrDefault(f => f.Card == card);

                if (sourceField == null || consideredField == null)
                    return false;

                var neighbourFields = gameEventArgs.Player.BoardSide.GetNeighbourFields(sourceField);
                return neighbourFields.Any(f => f == consideredField);
            };

            int value = 0;
            if (!Int32.TryParse(args[0], out value))
                return;

            foreach (IAttacker card in gameEventArgs.Player.AllCards.Where(c => c is IAttacker))
                card.AttackCalculators.Add((card => conditions.All(c => c.condition.Validate(gameEventArgs, c.args)) && isNeighbour(card),value));
        }
    }
}
