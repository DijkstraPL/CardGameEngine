using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_DataAccess.Entities.Enums
{
    [Flags]
    public enum InvocationTarget
    {
        None = 0 << 0,

        OwnLands = 1 << 0,
        OwnLand = 2 << 0,
        EnemyLands = 3 << 0,
        EnemyLand = 4 << 0,

        OwnEmptyField = 5 << 0,
        OwnUnit = 6 << 0,
        OwnStructure = 7 << 0,
        OwnCreature = 8 << 0,
        OwnHero = 9 << 0,
        OwnTakenField = 10 << 0,

        EnemyEmptyField = 11 << 0,
        EnemyUnit = 12 << 0,
        EnemyStructure = 13 << 0,
        EnemyCreature = 14 << 0,
        EnemyHero = 15 << 0,
        EnemyTakenField = 16 << 0,
    }
}
