﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Data.Data.Enums
{
    [Flags]
    public enum Trait
    {
        None = 0,

        DistanceAttack  = 1 << 0, // 1
        Defender        = 1 << 1, // 2
        Flying          = 1 << 2, // 4
        Legendary       = 1 << 3, // 8
        Protection      = 1 << 4  // 16
    }
}
