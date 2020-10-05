using System;

namespace CardGame_Data.GameData
{
    [Serializable]
    public class AttackTargetData
    {
        public PlayerData PlayerTarget { get; set; }
        public CardData CardTarget { get; set; }
    }
}
