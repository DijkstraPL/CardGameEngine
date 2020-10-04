using System.Windows;

namespace CardGame_Desktop.ViewModels
{
    public class LineViewModel
    {
        public Point Source { get; }
        public Point Target { get; }
        public FieldViewModel Field { get; }
        public FieldViewModel AttackTargetField { get; }

        public bool CanAttack => AttackTargetField == null ? true : Field.Field.CanAttack(AttackTargetField.Field); 

        public LineViewModel(Point source, Point target, FieldViewModel field, FieldViewModel attackTargetField)
        {
            Source = source;
            Target = target;
            Field = field;
            AttackTargetField = attackTargetField;
        }
    }
}