using System;
using System.Collections.Generic;

namespace CardGame_Data.GameData
{
    [Serializable]
    public class BoardSideData
    {
        public ICollection<CardData> LandCards { get; set; }
        public ICollection<FieldData> Fields { get; set; }
    }
}
