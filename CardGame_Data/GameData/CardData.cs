using CardGame_Data.Data.Enums;
using CardGame_Data.GameData.Enums;
using System;

namespace CardGame_Data.GameData
{
    [Serializable]
    public class CardData
    {
        public Guid Identifier { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int? Cost { get; set; }
        public InvocationTarget InvocationTarget { get; set; }
        public CardState CardState { get; set; }
        public Kind Kind { get; set; }
        public string Trait { get; set; }

        public string OwnerName { get; set; }

        public int? BaseCooldown { get; set; }
        public int? Cooldown { get; set; }

        public int? BaseAttack { get; set; }
        public int? FinalAttack { get; set; }

        public int? BaseHealth { get; set; }
        public int? FinalHealth { get; set; }

        public AttackTargetData AttackTarget { get; set; }
    }
}
