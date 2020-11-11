using CardGame_Data.Data.Enums;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Cards.Interfaces;
using CardGame_Game.Cards.Triggers;
using CardGame_Game.Cards.Triggers.Interfaces;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace CardGame_Game.BoardTable
{
    public class BoardSide : IBoardSide
    {
        public IReadOnlyList<Field> Fields { get; }
        public IList<GameLandCard> LandCards { get; } = new List<GameLandCard>();
        public IBoardSide EnemyBoardSide { get; set; }

        private readonly IGameEventsContainer _gameEventsContainer;

        public Field this[int columnIndex, int rowIndex]
        {
            get => Fields.FirstOrDefault(f => f.X == rowIndex && f.Y == columnIndex);
        }

        public BoardSide(IGameEventsContainer gameEventsContainer)
        {
            _gameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));

            Fields = new List<Field>
            {
                new Field(0,0),
                new Field(0,1),
                new Field(0,2),
                new Field(0,3),
                new Field(0,4),
                new Field(1,0),
                new Field(1,1),
                new Field(1,2),
                new Field(1,3),
                new Field(1,4),
                new Field(2,0),
                new Field(2,1),
                new Field(2,2),
                new Field(2,3),
                new Field(2,4),
            };
        }

        public IEnumerable<Field> GetNeighbourFields(Field field)
        {
            return Fields.Where(f =>
             f.X == field.X - 1 && f.Y == field.Y - 1 ||
             f.X == field.X - 1 && f.Y == field.Y + 1 ||
             f.X == field.X + 1 && f.Y == field.Y + 1 ||
             f.X == field.X + 1 && f.Y == field.Y - 1 ||
             f.X == field.X + 1 && f.Y == field.Y ||
             f.X == field.X - 1 && f.Y == field.Y ||
             f.X == field.X && f.Y == field.Y + 1 ||
             f.X == field.X && f.Y == field.Y - 1
             );
        }

        public IEnumerable<Field> GetNeighbourFieldsCross(Field field)
        {
            return Fields.Where(f =>
             f.X == field.X + 1 && f.Y == field.Y ||
             f.X == field.X - 1 && f.Y == field.Y ||
             f.X == field.X && f.Y == field.Y + 1 ||
             f.X == field.X && f.Y == field.Y - 1
             );
        }

        public void AddLandCard(GameLandCard card)
        {
            LandCards.Add(card);
        }

        public void StartTurn(IGame game)
        {
            foreach (var landCard in LandCards)
            {
                if (landCard.Cooldown == 0)
                    landCard.Cooldown = landCard.BaseCooldown;
                landCard.Cooldown--;
            }

            foreach (var field in Fields)
            {
                if (field.Card != null)
                {
                    if (field.Card.Cooldown == 0)
                        field.Card.Cooldown = field.Card.BaseCooldown;
                    field.Card.Cooldown--;

                    field.Card.Contrattacked = false;
                    if (field.Card is IMovable movableCard)
                        movableCard.Moved = false;
                }
            }
        }

        public void Move(IPlayer player, Field start, Field target)
        {
            if (player.CanMove() &&
                target.Card == null &&
                GetNeighbourFieldsCross(start).Contains(target) &&
                start.Card is IMovable movableCard &&
                !movableCard.Moved)
            {
                player.OnCardMove();
                var card = start.Card;
                target.Card = card;
                start.Card = null;
                movableCard.Moved = true;
            }
        }

        public void FinishTurn(IGame game, IPlayer player)
        {
            var enemyPlayer = game.CurrentPlayer == player ? game.NextPlayer : game.CurrentPlayer;

            foreach (var field in Fields)
            {
                if (field.Card == null || field.Card.Cooldown > 0)
                    continue;

                if (field.Card.AttackTarget is IPlayer targetPlayer &&  field.CanAttack(targetPlayer))
                {
                    AttackPlayer(game, player,  field, targetPlayer);
                }
                else if (field.Card.AttackTarget is GameUnitCard attackTarget)
                {
                    var targetField = game.NextPlayer.BoardSide.Fields.FirstOrDefault(f => f.Card == field.Card.AttackTarget);
                    if (targetField == null)
                        continue;
                    if (!field.CanAttack(targetField, enemyPlayer ))
                        continue;

                    AttackUnit(game, player, field, targetField);
                }
            }
        }

        private void AttackUnit(IGame game, IPlayer player, Field field, Field targetField)
        {
            var attackTarget = targetField.Card;

            game.GameEventsContainer.UnitBeingAttackingEvent.Raise(this,
                new GameEventArgs { Game = game, Player = player, SourceCard = attackTarget, Targets = new List<GameCard> { field.Card } });

            if (field.Card != null && field.Card.FinalHealth > 0)
            {
                field.Card.AttackTarget.AddHealthCalculation((card => true, -field.Card.FinalAttack ?? 0));

                if (!targetField.CanContrattackAttackFrom(field))
                    field.Card.AttackTarget = null;
                else
                {
                    field.Card.AttackTarget.Contrattacked = true;
                    field.Card.AddHealthCalculation((card => true, -attackTarget.FinalAttack / 2 ?? 0));
                }

                field.Card.AttackTarget = null;
            }

            game.GameEventsContainer.UnitAttackedEvent.Raise(this,
                new GameEventArgs { Game = game, Player = player, SourceCard = field.Card, Targets = new List<GameCard> { attackTarget } });
        }

        private void AttackPlayer(IGame game, IPlayer player, Field field, IPlayer enemyPlayer)
        {
            if (field.CanAttack(enemyPlayer))
            {
                game.NextPlayer.AddHealthCalculation((card => true, -field.Card.FinalAttack ?? 0));

                game.GameEventsContainer.UnitAttackedEvent.Raise(this,
                    new GameEventArgs { Game = game, Player = player, SourceCard = field.Card });
            }
        }

        public void Kill(GameUnitCard gameUnitCard)
        {
            var field = Fields.FirstOrDefault(f => f.Card == gameUnitCard);
            field.Card = null;
            _gameEventsContainer.UnitKilledEvent.Raise(this, new GameEventArgs { SourceCard = gameUnitCard });
        }
    }
}
