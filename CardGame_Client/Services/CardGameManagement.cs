﻿using CardGame_Client.Events;
using CardGame_Client.Services.Interfaces;
using CardGame_Data.Data.Enums;
using CardGame_Data.GameData;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Client.Services
{
    public class CardGameManagement : ICardGameManagement
    {
        public SelectionTargetData SelectionTargetData { get; private set; }

        private readonly IClientGameManager _clientGameManager;
        private readonly ITargetSelectionManagement _targetSelectionManagement;
        private CardData _cardDataToBePlay;
        private CardData _attackSource;
        private CardData _attackTarget;

        public CardGameManagement(IClientGameManager clientGameManager, ITargetSelectionManagement targetSelectionManagement)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _targetSelectionManagement = targetSelectionManagement ?? throw new ArgumentNullException(nameof(targetSelectionManagement));
        }

        public bool HasTarget(CardData cardData)
        {
            if (cardData.InvocationTarget == InvocationTarget.None)
                return true;

            foreach (InvocationTarget invocationTarget in (InvocationTarget[])Enum.GetValues(typeof(InvocationTarget)))
            {
                if (invocationTarget == InvocationTarget.None)
                    continue;
                if (cardData.InvocationTarget.HasFlag(invocationTarget) &&
                    CheckForFlag(invocationTarget))
                    return true;
            }
            return false;
        }

        private bool CheckForFlag(InvocationTarget invocationTarget)
        {
            switch (invocationTarget)
            {
                case InvocationTarget.None:
                    return true;
                case InvocationTarget.OwnLands:
                    throw new NotImplementedException();
                case InvocationTarget.OwnLand:
                    throw new NotImplementedException();
                case InvocationTarget.EnemyLands:
                    throw new NotImplementedException();
                case InvocationTarget.EnemyLand:
                    throw new NotImplementedException();
                case InvocationTarget.OwnEmptyField:
                    return SelectionTargetData?.TargetOwnField != null &&
                        SelectionTargetData.TargetOwnField.UnitCard == null;
                case InvocationTarget.OwnUnit:
                    return SelectionTargetData?.TargetOwnField?.UnitCard != null &&
                        (SelectionTargetData.TargetOwnField.UnitCard.Kind == Kind.Creature ||
                        SelectionTargetData.TargetOwnField.UnitCard.Kind == Kind.Structure);
                case InvocationTarget.OwnStructure:
                    return SelectionTargetData?.TargetOwnField?.UnitCard != null &&
                        SelectionTargetData.TargetOwnField.UnitCard.Kind == Kind.Structure;
                case InvocationTarget.OwnCreature:
                    return SelectionTargetData?.TargetOwnField?.UnitCard != null &&
                        SelectionTargetData.TargetOwnField.UnitCard.Kind == Kind.Creature;
                case InvocationTarget.OwnHero:
                    throw new NotImplementedException();
                case InvocationTarget.OwnTakenField:
                    throw new NotImplementedException();
                case InvocationTarget.EnemyEmptyField:
                    throw new NotImplementedException();
                case InvocationTarget.EnemyUnit:
                    return SelectionTargetData?.TargetEnemyField?.UnitCard != null &&
                        (SelectionTargetData.TargetEnemyField.UnitCard.Kind == Kind.Creature ||
                        SelectionTargetData.TargetEnemyField.UnitCard.Kind == Kind.Structure);
                case InvocationTarget.EnemyStructure:
                    return SelectionTargetData?.TargetEnemyField?.UnitCard != null &&
                        SelectionTargetData.TargetEnemyField.UnitCard.Kind == Kind.Structure;
                case InvocationTarget.EnemyCreature:
                    return SelectionTargetData?.TargetEnemyField?.UnitCard != null &&
                        SelectionTargetData.TargetEnemyField.UnitCard.Kind == Kind.Creature;
                case InvocationTarget.EnemyHero:
                    throw new NotImplementedException();
                case InvocationTarget.EnemyTakenField:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public void SetTarget(CardData cardData)
        {
            _cardDataToBePlay = cardData;
        }

        public void OnFieldSelected(object sender, FieldSelectorEventArgs eventArgs)
        {
            if (_cardDataToBePlay != null)
            {
                SelectionTargetData = new SelectionTargetData();
                if (eventArgs.IsEnemyField)
                    SelectionTargetData.TargetEnemyField = eventArgs.FieldData;
                if (!eventArgs.IsEnemyField)
                    SelectionTargetData.TargetOwnField = eventArgs.FieldData;

                if (HasTarget(_cardDataToBePlay))
                {
                    _clientGameManager.PlayCard(_cardDataToBePlay, SelectionTargetData);
                    ClearTargets();
                }
            }
            else
            {
                if (!eventArgs.IsEnemyField && eventArgs.FieldData.UnitCard != null)
                    _attackSource = eventArgs.FieldData.UnitCard;
                else if (eventArgs.IsEnemyField && eventArgs.FieldData.UnitCard != null && _attackSource != null)
                    _attackTarget = eventArgs.FieldData.UnitCard;
                else if (eventArgs.FieldData.UnitCard == null && _attackSource != null && _targetSelectionManagement.CanMove(_attackSource, eventArgs.FieldData))
                {
                    _clientGameManager.Move(_attackSource, eventArgs.FieldData);
                    _attackSource = null;
                }

                if (_attackSource != null && _attackTarget != null)
                {
                    _clientGameManager.SetAttackTarget(_attackSource, _attackTarget);
                    _attackSource = null;
                    _attackTarget = null;
                }
            }
        }

        public void OnPlayerSelected(PlayerData playerData)
        {
            if (playerData != null && _attackSource != null)
            {
                _clientGameManager.SetAttackTarget(_attackSource, playerData);
                _attackSource = null;
                _attackTarget = null;
            }
        }

        public void ClearTargets()
        {
            _cardDataToBePlay = null;
            SelectionTargetData = null;
        }
    }
}
