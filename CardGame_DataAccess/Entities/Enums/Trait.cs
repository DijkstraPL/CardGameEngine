using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_DataAccess.Entities.Enums
{
    [Flags]
    public enum Trait
    {
        None = 0,

        DistanceAttack  = 1 << 0, // 1
        Defender        = 1 << 1, // 2
        Flying          = 1 << 2  // 4
    }
}
