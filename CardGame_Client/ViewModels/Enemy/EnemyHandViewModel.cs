using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame_Client.ViewModels.Enemy
{
    public class EnemyHandViewModel : BindableBase
    {
        private IEnumerable<int> _hand;
        public IEnumerable<int> Hand
        {
            get => _hand;
            private set => SetProperty(ref _hand, value);
        }

        private readonly IClientGameManager _clientGameManager;
        private GameData _gameData;
        private PlayerData _player;

        public EnemyHandViewModel(IClientGameManager clientGameManager)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _clientGameManager.CardTaken += OnCardTaken;
            _clientGameManager.TurnStarted += OnTurnStarted;
            _clientGameManager.CardPlayed += OnCardPlayed;

            SetHand(_clientGameManager.GameData);
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
            _player = _gameData.IsControllingCurrentPlayer ? _gameData.NextPlayer : _gameData.CurrentPlayer;

            Hand = Enumerable.Range(0, _player.Hand);
        }
    }
}
