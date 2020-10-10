﻿using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace CardGame_Client.ViewModels.Enemy
{
    public class EnemyBoardViewModel : BindableBase
    {
        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set => SetProperty(ref _playerName, value);
        }

        private IList<LandCardViewModel> _landCards = new ObservableCollection<LandCardViewModel>();
        public IEnumerable<LandCardViewModel> LandCards => _landCards;
        private IList<BoardFieldViewModel> _fields = new ObservableCollection<BoardFieldViewModel>();
        public IEnumerable<BoardFieldViewModel> Fields => _fields;

        public ICommand SelectToAttackPlayerCommand { get; }

        private readonly IClientGameManager _clientGameManager;
        private readonly ICardGameManagement _cardGameManagement;
        private GameData _gameData;
        private PlayerData _player;

        public EnemyBoardViewModel(IClientGameManager clientGameManager, ICardGameManagement cardGameManagement)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _cardGameManagement = cardGameManagement ?? throw new ArgumentNullException(nameof(cardGameManagement));
            _clientGameManager.CardTaken += OnCardTaken;
            _clientGameManager.TurnStarted += OnTurnStarted;
            _clientGameManager.CardPlayed += OnCardPlayed;

            _gameData = _clientGameManager.GameData ?? throw new ArgumentNullException(nameof(_clientGameManager.GameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.NextPlayer : _gameData.CurrentPlayer;
            PlayerName = _player.Name;

            SetLandCards(_clientGameManager.GameData);
            SetFields(_clientGameManager.GameData);
        }

        private void OnCardPlayed(object sender, GameData gameData)
        {
            SetLandCards(gameData);
            SetFields(gameData);
        }

        private void OnCardTaken(object sender, GameData gameData)
        {
            SetLandCards(gameData);
            SetFields(gameData);
        }

        private void OnTurnStarted(object sender, GameData gameData)
        {
            SetLandCards(gameData);
            SetFields(gameData);
        }

        private void SetLandCards(GameData gameData)
        {
            _gameData = gameData ?? throw new ArgumentNullException(nameof(gameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.NextPlayer : _gameData.CurrentPlayer;

            _landCards.Clear();
            foreach (var landCard in _player.BoardSide.LandCards ?? new List<CardData>())
                _landCards.Add(new LandCardViewModel(landCard));
        }

        private void SetFields(GameData gameData)
        {
            _gameData = gameData ?? throw new ArgumentNullException(nameof(gameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.NextPlayer : _gameData.CurrentPlayer;

            _fields.Clear();
            foreach (var field in _player.BoardSide.Fields)
                _fields.Add(new BoardFieldViewModel(field, isEnemyField: true, _cardGameManagement));
        }
    }
}
