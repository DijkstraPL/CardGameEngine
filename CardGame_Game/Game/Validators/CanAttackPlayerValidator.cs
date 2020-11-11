using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable;
using CardGame_Game.Players.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame_Game.Game.Validators
{
    public class CanAttackPlayerValidator
    {
        public bool Validate(Field sourceField, IPlayer targetPlayer)
        {
            if (sourceField?.Card == null || targetPlayer == null)
                return false;
            return HasFlying(sourceField) || IsInAttackPosition(sourceField) && !HasBlockerCard(targetPlayer);
        }

        private bool HasFlying(Field sourceField)
        {
            return sourceField.Card.Trait.HasFlag(Trait.Flying);
        }

        private bool HasBlockerCard(IPlayer targetPlayer)
        {
            return targetPlayer.BoardSide.Fields
                      .Where(f => f.X == 2)
                      .Any(f => f.Card != null);
        }

        private bool IsInAttackPosition(Field sourceField)
        {
            return sourceField.X == 0;
        }
    }
}
