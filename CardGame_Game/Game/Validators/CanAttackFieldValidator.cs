using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable;
using CardGame_Game.Players.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.Game.Validators
{
    public class CanAttackFieldValidator
    {
        public bool Validate(Field sourceField, Field targetField, IPlayer enemyPlayer)
        {
            if (sourceField?.Card == null || targetField?.Card == null || enemyPlayer == null)
                return false;

            return IsNeighbour(sourceField, targetField ) && 
                (IsTargetDefender(targetField) ||
                !HasDefender(targetField, enemyPlayer) ||
                IsFlying(sourceField));
        }

        private bool IsTargetDefender(Field targetField)
        {
            return targetField.Card.Trait.HasFlag(Trait.Defender);
        }
        private bool HasDefender(Field targetField, IPlayer enemyPlayer)
        {
            var enemyFields = enemyPlayer.BoardSide.Fields.Where(f => f.X < targetField.X && 
                (f.Y == targetField.Y - 1 || f.Y == targetField.Y || f.Y == targetField.Y + 1));

            return enemyFields.Any(f => f.Card?.Trait.HasFlag(Trait.Defender) ?? false);
        }

        private bool IsFlying(Field sourceField)
        {
           return sourceField.Card.Trait.HasFlag(Trait.Flying);
        }

        private bool IsNeighbour(Field sourceField, Field targetField)
        {
            return sourceField.Y - 1 <= targetField.Y &&
                targetField.Y <= sourceField.Y + 1;
        }
    }
}
