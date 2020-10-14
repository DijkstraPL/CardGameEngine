using CardGame_Client.Services.Interfaces;
using CardGame_Client.ViewModels;
using CardGame_Client.ViewModels.Enemy;
using CardGame_Client.ViewModels.Interfaces;
using CardGame_Client.ViewModels.Player;
using CardGame_Data.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_Client.Services
{
    public class TargetSelectionManagement : ITargetSelectionManagement
    {
        private PlayerBoardViewModel _playerBoardViewModel;
        private EnemyBoardViewModel _enemyBoardViewModel;
        private GameViewModel _gameViewModel;
        private readonly IClientGameManager _clientGameManager;

        public TargetSelectionManagement(IClientGameManager clientGameManager)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));

            _clientGameManager.AttackTargetSet += OnAttackTargetSet;
            _clientGameManager.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(object sender, GameData gameData)
        {
            _gameViewModel.ClearLines();

            var player = gameData.IsControllingCurrentPlayer ? gameData.CurrentPlayer : gameData.NextPlayer;
            var nextPlayer = gameData.IsControllingCurrentPlayer ? gameData.NextPlayer : gameData.CurrentPlayer;
            SetAttackTargets(player, nextPlayer, isPlayerSource: true);
            SetAttackTargets(nextPlayer, player, isPlayerSource: false);
        }

        public void SetPlayerBoardViewModel(PlayerBoardViewModel playerBoardViewModel)
        {
            _playerBoardViewModel = playerBoardViewModel ?? throw new ArgumentNullException(nameof(playerBoardViewModel));
        }
        public void SetEnemyBoardViewModel(EnemyBoardViewModel enemyBoardViewModel)
        {
            _enemyBoardViewModel = enemyBoardViewModel ?? throw new ArgumentNullException(nameof(enemyBoardViewModel));
        }

        public void SetGameViewModel(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel ?? throw new ArgumentNullException(nameof(gameViewModel));
        }

        private void OnAttackTargetSet(object sender, GameData gameData)
        {
            _gameViewModel.ClearLines();

            var player = gameData.IsControllingCurrentPlayer ? gameData.CurrentPlayer : gameData.NextPlayer;
            var nextPlayer = gameData.IsControllingCurrentPlayer ? gameData.NextPlayer : gameData.CurrentPlayer;
            SetAttackTargets(player, nextPlayer, isPlayerSource: true);
            SetAttackTargets(nextPlayer, player, isPlayerSource: false);
        }

        private void SetAttackTargets(PlayerData sourcePlayer, PlayerData targetPlayer, bool isPlayerSource)
        {
            foreach (var field in sourcePlayer.BoardSide.Fields.Where(f => f.UnitCard?.AttackTarget != null))
            {
                if (field.UnitCard.AttackTarget.CardTargetIdentifier != default)
                {
                    var targetField = targetPlayer.BoardSide.Fields.FirstOrDefault(f => f.UnitCard?.Identifier == field.UnitCard.AttackTarget.CardTargetIdentifier);
                    if (targetField != null)
                        SetAttackTarget(field.UnitCard, targetField.UnitCard, isPlayerSource);
                }
                else if (field.UnitCard.AttackTarget.PlayerTargetName != null)
                    SetAttackTarget(field.UnitCard, field.UnitCard.AttackTarget.PlayerTargetName == sourcePlayer.Name ? sourcePlayer : targetPlayer, isPlayerSource);
            }
        }

        private void SetAttackTarget(CardData attackSource, PlayerData playerData, bool isPlayerSource)
        {
            BoardFieldViewModel source = null;
            IPosition target = null;
            if (isPlayerSource)
            {
                source = _playerBoardViewModel.Fields.FirstOrDefault(f => f.Field.UnitCard?.Identifier == attackSource.Identifier);
                target = _enemyBoardViewModel;
            }
            else
            {
                source = _enemyBoardViewModel.Fields.FirstOrDefault(f => f.Field.UnitCard?.Identifier == attackSource.Identifier);
                target = _playerBoardViewModel;
            }

            if (source == null || target == null)
                return;

            var lineViewModel = new LineViewModel(source, target, canAttack: true);
            _gameViewModel.SetLine(source, lineViewModel);
        }

        private void SetAttackTarget(CardData attackSource, CardData attackTarget, bool isPlayerSource)
        {
            BoardFieldViewModel source = null;
            BoardFieldViewModel target = null;
            if (isPlayerSource)
            {
                source = _playerBoardViewModel.Fields.FirstOrDefault(f => f.Field.UnitCard?.Identifier == attackSource.Identifier);
                target = _enemyBoardViewModel.Fields.FirstOrDefault(f => f.Field.UnitCard?.Identifier == attackTarget.Identifier);
            }
            else
            {
                source = _enemyBoardViewModel.Fields.FirstOrDefault(f => f.Field.UnitCard?.Identifier == attackSource.Identifier);
                target = _playerBoardViewModel.Fields.FirstOrDefault(f => f.Field.UnitCard?.Identifier == attackTarget.Identifier);
            }

            if (source == null || target == null)
                return;

            var lineViewModel = new LineViewModel(source, target, canAttack: CanAttack(source.Field, target.Field));
            _gameViewModel.SetLine(source, lineViewModel);
        }

        private bool CanAttack(FieldData sourceField, FieldData targetField)
        {
            return sourceField.Y - 1 <= targetField.Y &&
                       targetField.Y <= sourceField.Y + 1;
        }

        public bool CanMove(CardData attackSource, FieldData targetField)
        {
            var fields = _playerBoardViewModel.PlayerName == attackSource.OwnerName ? _playerBoardViewModel.Fields : _enemyBoardViewModel.Fields;

            var sourceField = fields.FirstOrDefault(f => f.Field.UnitCard == attackSource);

            if(sourceField != null && targetField != null && targetField.UnitCard == null)
            {
                return targetField.X == sourceField.Field.X + 1 && targetField.Y == sourceField.Field.Y ||
                      targetField.X == sourceField.Field.X - 1 && targetField.Y == sourceField.Field.Y ||
                      targetField.Y == sourceField.Field.Y + 1 && targetField.X == sourceField.Field.X ||
                      targetField.Y == sourceField.Field.Y - 1 && targetField.X == sourceField.Field.X;
            }

            return false;
        }
    }
}
