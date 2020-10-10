using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Data.Data.Enums
{
    [Flags]
    public enum InvocationTarget
    {
        None =            0,
                          
        OwnLands =        1 << 0, // 1
        OwnLand =         1 << 1, // 2
        EnemyLands =      1 << 2, // 4
        EnemyLand =       1 << 3, // 8
                          
        OwnEmptyField =   1 << 4, // 16
        OwnUnit =         1 << 5, // 32
        OwnStructure =    1 << 6, // 64
        OwnCreature =     1 << 7, // 128
        OwnHero =         1 << 8, // 256
        OwnTakenField =   1 << 9, // 514

        EnemyEmptyField = 1 << 10, // 1024
        EnemyUnit =       1 << 11, // 2048
        EnemyStructure =  1 << 12, // 4096
        EnemyCreature =   1 << 13, // 8192
        EnemyHero =       1 << 14, // 16384
        EnemyTakenField = 1 << 15, // 32768
    }
}
