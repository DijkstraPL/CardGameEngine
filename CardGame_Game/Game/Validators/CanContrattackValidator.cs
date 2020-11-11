using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable;

namespace CardGame_Game.Game.Validators
{
    public class CanContrattackValidator
    {
        public bool Validate(Field sourceField, Field targetField)
        {
            if (sourceField?.Card == null || targetField?.Card == null)
                return false;

            return !TargetContrattacked(targetField) &&
                !IsSourceRanger(sourceField) &&
                (!IsFlying(sourceField) || IsFlying(targetField)) &&
                IsFrontLiner(targetField);
        }

        private bool TargetContrattacked(Field targetField)
        {
            return targetField.Card.Contrattacked;
        }

        private bool IsSourceRanger(Field sourceField)
        {
            return sourceField.Card.Trait.HasFlag(Trait.DistanceAttack);
        }
        private bool IsFlying(Field field)
        {
            return field.Card.Trait.HasFlag(Trait.Flying);
        }

        private bool IsFrontLiner(Field targetField)
        {
            return targetField.X == 0;
        }
    }
}
