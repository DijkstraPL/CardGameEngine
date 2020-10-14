using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_Client.ViewModels.Enemy
{
    public class EnemyDataViewModel : BindableBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }
        private int? _morale;
        public int? Morale
        {
            get => _morale;
            private set => SetProperty(ref _morale, value);
        }
        private int? _energy;
        public int? Energy
        {
            get => _energy;
            private set => SetProperty(ref _energy, value);
        }
        private int? _finalHealth;
        public int? FinalHealth
        {
            get => _finalHealth;
            private set => SetProperty(ref _finalHealth, value);
        }

        private readonly IClientGameManager _clientGameManager;
        private GameData _gameData;
        private PlayerData _player;

        public EnemyDataViewModel(IClientGameManager clientGameManager)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _clientGameManager.TurnStarted += OnTurnStarted;
            _clientGameManager.CardPlayed += OnCardPlayed;
            _clientGameManager.CardMoved += OnCardMoved;

            SetPlayerInfo(_clientGameManager.GameData);
        }

        private void OnCardMoved(object sender, GameData gameData)
        {
            SetPlayerInfo(gameData);
        }

        private void OnCardPlayed(object sender, GameData gameData)
        {
            SetPlayerInfo(gameData);
        }

        private void OnTurnStarted(object sender, GameData gameData)
        {
            SetPlayerInfo(gameData);
        }

        private void SetPlayerInfo(GameData gameData)
        {
            _gameData = gameData ?? throw new ArgumentNullException(nameof(gameData));
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.NextPlayer : _gameData.CurrentPlayer;

            Name = _player.Name;
            Morale = _player.Morale;
            Energy = _player.Energy;
            FinalHealth = _player.FinalHealth;
        }
    }
}
