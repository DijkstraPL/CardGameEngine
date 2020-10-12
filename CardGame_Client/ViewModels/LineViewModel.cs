using CardGame_Client.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CardGame_Client.ViewModels
{
    public class LineViewModel
    {
        public IPosition AttackSourceField { get; }
        public IPosition AttackTargetField { get; }

        public bool CanAttack { get; }

        public LineViewModel(IPosition attackSourceField, IPosition attackTargetField, bool canAttack)
        {
            AttackSourceField = attackSourceField ?? throw new ArgumentNullException(nameof(attackSourceField));
            AttackTargetField = attackTargetField ?? throw new ArgumentNullException(nameof(attackTargetField));

            CanAttack = canAttack;
        }
    }
}
