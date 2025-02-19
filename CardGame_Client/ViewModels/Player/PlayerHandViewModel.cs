﻿using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CardGame_Client.ViewModels.Player
{
    public class PlayerHandViewModel : BindableBase
    {
        private ICollection<CardData> _hand;
        public ICollection<CardData> Hand
        {
            get => _hand;
            private set => SetProperty(ref _hand, value);
        }

        public ICommand PlayCardCommand { get; }

        private readonly IClientGameManager _clientGameManager;
        private readonly ICardGameManagement _cardGameManagement;
        private GameData _gameData;
        private PlayerData _player;

        public PlayerHandViewModel(IClientGameManager clientGameManager, ICardGameManagement cardGameManagement)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _cardGameManagement = cardGameManagement ?? throw new ArgumentNullException(nameof(cardGameManagement));
            _clientGameManager.CardTaken += OnCardTaken;
            _clientGameManager.TurnStarted += OnTurnStarted;
            _clientGameManager.CardPlayed += OnCardPlayed;

            SetHand(_clientGameManager.GameData);

            PlayCardCommand = new DelegateCommand<CardData>(PlayCard);
        }

        private void PlayCard(CardData cardData)
        {
            if (_cardGameManagement.HasTarget(cardData))
            {
                _clientGameManager.PlayCard(cardData, _cardGameManagement.SelectionTargetData);
                _cardGameManagement.ClearTargets();
            }
            else
                _cardGameManagement.SetTarget(cardData);
        }

        private void OnCardPlayed(object sender, GameData gameData)
        {
            SetHand(gameData);
        }

        private void OnTurnStarted(object sender, GameData gameData)
        {
            SetHand(gameData);
        }

        private void OnCardTaken(object sender, GameData gameData)
        {
            SetHand(gameData);
        }

        private void SetHand(GameData gameData)
        {
            _gameData = gameData ?? throw new ArgumentNullException(nameof(gameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.CurrentPlayer : _gameData.NextPlayer;

            Hand = _player.HandCards;
        }

    }
}
