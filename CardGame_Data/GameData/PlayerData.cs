using CardGame_Data.Data.Enums;
using System;
using System.Collections.Generic;

namespace CardGame_Data.GameData
{
    [Serializable]
    public class PlayerData
    {
        public string Name { get; set; }
        public int LandDeck { get; set; }
        public int Deck { get; set; }
        public int Graveyard { get; set; }
        public int Hand { get; set; }
        public CardColor PlayerColor { get; set; }
        public int Energy { get; set; }
        public BoardSideData BoardSide { get; set; }
        public int? BaseHealth { get; set; }
        public int? FinalHealth { get; set; }
        public ICollection<CardData> HandCards { get; set; }

        public int? Morale { get; set; }
    }
}
