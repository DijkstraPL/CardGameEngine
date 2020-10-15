using CardGame_Data.Data.Enums;
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
    /// AddHealth({'NEIGHBOURS'},{Amount},{TurnsAmount/'INFINITE'})
    /// </summary>
    [Export(Name, typeof(IEffect))]
    public class AddHealth : IEffect
    {
        public const string Name = "AddHealth";
        string IEffect.Name => Name;

        [ImportingConstructor]
        public AddHealth()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args)
        {

            if (args[2] != "INFINITE")
                throw new NotImplementedException();

            if (args[0] == "NEIGHBOURS" &&
            Int32.TryParse(args[1], out int neighbourHealthAddition))
            {
                var field = gameEventArgs.Player.BoardSide.Fields.FirstOrDefault(f => f.Card == gameEventArgs.SourceCard);
                if (field != null)
                {
                    var fields = gameEventArgs.Player.BoardSide.GetNeighbourFields(field);
                    fields.Where(f => f.Card != null)
                        .ToList()
                        .ForEach(f => f.Card.HealthCalculators.Add((card => true, neighbourHealthAddition)));
                }
            }
            else if (args[0] == "SELF" &&
                Int32.TryParse(args[1], out int selfHealthAddition) &&
                gameEventArgs.SourceCard is GameUnitCard gameUnitCard)
                gameUnitCard.HealthCalculators.Add((card => conditions.All(c => c.condition.Validate(gameEventArgs, c.args)), selfHealthAddition));
            else if (args[0] == "TARGET" &&
                Int32.TryParse(args[1], out int targetHealthAddition) &&
                gameEventArgs.Targets.FirstOrDefault() is GameUnitCard targetGameUnitCard)
                targetGameUnitCard.HealthCalculators.Add((card => conditions.All(c => c.condition.Validate(gameEventArgs, c.args)), targetHealthAddition));
            else if (args[0] == "ALLFRIENDLYCREATURES" &&
                  Int32.TryParse(args[1], out int creaturesHealthAddition))
            {
                foreach (IHealthy card in gameEventArgs.Player.AllCards.Where(c => c.Kind == Kind.Creature && c is IHealthy))                
                    card.HealthCalculators.Add((card => conditions.All(c => c.condition.Validate(gameEventArgs, c.args)), creaturesHealthAddition));
            }
        }
    }
}
