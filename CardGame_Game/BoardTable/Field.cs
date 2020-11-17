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
        }

        public bool CanAttack(IPlayer targetPlayer)
        {
            return _canAttackPlayerValidator.Validate(this, targetPlayer);
        }
    }
}
