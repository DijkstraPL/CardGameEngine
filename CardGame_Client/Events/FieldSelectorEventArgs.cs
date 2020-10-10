using CardGame_Data.GameData;
using System;

namespace CardGame_Client.Events
{
    public class FieldSelectorEventArgs : EventArgs
    {
        public FieldData FieldData { get;}
        public bool IsEnemyField { get;  }

        public FieldSelectorEventArgs(FieldData fieldData, bool isEnemyField)
        {
            FieldData = fieldData ?? throw new ArgumentNullException(nameof(fieldData));
            IsEnemyField = isEnemyField;
        }
    }
}
