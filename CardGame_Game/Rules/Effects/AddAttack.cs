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
            Func<bool> predicate = () => true;
            if (Int32.TryParse(args[2], out int turnsAmount) && _turnsLeft == null)
            {
                _turnsLeft = turnsAmount;
                _turnStamp = gameEventArgs.Game.TurnCounter;
            }
            if (_turnsLeft != null)
                predicate = () => _turnStamp + _turnsLeft > gameEventArgs.Game.TurnCounter;

            if (args[0] == "SELF" && Int32.TryParse(args[1], out int value) && gameEventArgs.SourceCard is IAttacker attacker)
                attacker.AttackCalculators.Add((() => conditions.All(c => c.condition.Validate(gameEventArgs, c.args)) && predicate(), value));
            else if (args[0] == "TARGET" && Int32.TryParse(args[1], out int targetAttackValue) && gameEventArgs.Targets.FirstOrDefault() is IAttacker targetAttacker)
                targetAttacker.AttackCalculators.Add((() => conditions.All(c => c.condition.Validate(gameEventArgs, c.args)) && predicate(), targetAttackValue));

        }
    }
}
