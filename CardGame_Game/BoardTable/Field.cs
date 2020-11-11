using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Game.Validators;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame_Game.BoardTable
{
    public class Field : IField
    {
        public Guid Identifier { get; }
        public int X { get; }
        public int Y { get; }

        public GameUnitCard Card { get; set; }

        private CanAttackFieldValidator _canAttackFieldValidator = new CanAttackFieldValidator();
        private CanAttackPlayerValidator _canAttackPlayerValidator = new CanAttackPlayerValidator();
        private CanContrattackValidator _canContrattackValidator = new CanContrattackValidator();

        public Field(int x, int y)
        {
            Identifier = Guid.NewGuid();

            X = x;
            Y = y;
        }

        public bool CanContrattackAttackFrom(Field sourceField)
        {
            return _canContrattackValidator.Validate(sourceField, this);
        }

        public bool CanAttack(Field targetField, IPlayer enemyPlayer)
        {
            return _canAttackFieldValidator.Validate(this, targetField, enemyPlayer);

            //bool isDefender = targetField.Card?.Trait.HasFlag(Trait.Defender) ?? false;
            //bool hasDefender = enemyFields.Where(f => (f.Y == Y || f.Y == Y - 1 || f.Y == Y + 1) && f.X > X)
            //    .Any(f => f.Card?.Trait.HasFlag(Trait.Defender) ?? false);

            //bool isNeighbour = Y - 1 <= targetField.Y &&
            //           targetField.Y <= Y + 1;

            //bool hasFlying = Card?.Trait.HasFlag(Trait.Flying) ?? false;

            //return isNeighbour && (!hasDefender || isDefender) || hasFlying ;
        }

        public bool CanAttack(IPlayer targetPlayer)
        {
            return _canAttackPlayerValidator.Validate(this, targetPlayer);
        }
    }
}
