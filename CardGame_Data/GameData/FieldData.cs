using System;

namespace CardGame_Data.GameData
{
    [Serializable]
    public class FieldData
    {
        public Guid Identifier { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public CardData UnitCard { get; set; }
    }
}
