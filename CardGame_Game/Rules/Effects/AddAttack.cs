using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace CardGame_Game.Rules.Effects
{
    /// <summary>
    /// AddAttack({'TARGET'/'SELF'},{Amount},{TurnsAmount/'INFINITE'})
    /// </summary>
    [Export(Name, typeof(IEffect))]
    public class AddAttack : IEffect
    {
        public const string Name = "AddAttack";
        string IEffect.Name => Name;

        private int? _turnsLeft;
        private int _turnStamp;

        [ImportingConstructor]
        public AddAttack()
        {
        }

        public void Invoke(GameEventArgs gameEventArgs, IEnumerable<(ICondition condition, string[] args)> conditions, params string[] args)
        {
            Func<bool> turnCounterPredicate = () => true;
            if (Int32.TryParse(args[2], out int turnsAmount) && _turnsLeft == null)
            {
                _turnsLeft = turnsAmount;
                _turnStamp = gameEventArgs.Game.TurnCounter;
            }
            if (_turnsLeft != null)
                turnCounterPredicate = () => _turnStamp + _turnsLeft > gameEventArgs.Game.TurnCounter;

            int value = 0;
            if (!Int32.TryParse(args[1], out value))
                return;

            if (args[0] == "SELF" && gameEventArgs.SourceCard is IAttacker attacker)
                attacker.AttackCalculators.Add((card => conditions.All(c => c.condition.Validate(gameEventArgs, c.args)) && turnCounterPredicate(), value));
            else if (args[0] == "TARGET" && gameEventArgs.Targets.FirstOrDefault() is IAttacker targetAttacker)
                targetAttacker.AttackCalculators.Add((card => conditions.All(c => c.condition.Validate(gameEventArgs, c.args)) && turnCounterPredicate(), value));
         
        }
    }
}
