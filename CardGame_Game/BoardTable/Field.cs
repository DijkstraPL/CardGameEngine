using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.BoardTable
{
    public class Field : IField
    {
        public int X { get; }
        public int Y { get; }

        public GameUnitCard Card { get; set; }

        public Field(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool CanAttack(Field targetField, IEnumerable<Field> enemyFields)
        {
            bool isDefender = targetField.Card?.Trait.HasFlag(Trait.Defender) ?? false;
            bool hasDefender = enemyFields.Where(f => f.Y == Y || f.Y == Y - 1 || f.Y == Y + 1)
                .Any(f => f.Card?.Trait.HasFlag(Trait.Defender) ?? false);

            bool isNeighbour = Y - 1 <= targetField.Y &&
                       targetField.Y <= Y + 1;

            return isNeighbour && (!hasDefender || isDefender);
        }
    }
}
