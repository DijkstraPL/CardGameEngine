using System;

namespace CardGame_Data.GameData
{
    [Serializable]
    public class AttackTargetData
    {
        public string PlayerTargetName { get; set; }
        public Guid CardTargetIdentifier { get; set; }
        public bool CanAttack { get; set; }
    }
}
