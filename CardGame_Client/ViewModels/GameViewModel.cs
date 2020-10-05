using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CardGame_Client.ViewModels
{
    public class GameViewModel:BindableBase
    {
        private readonly IList<CardData> _hand = new ObservableCollection<CardData>();
        public IEnumerable<CardData> Hand => _hand;

        private readonly IClientGameManager _clientGameManager;

        public GameViewModel(IClientGameManager clientGameManager)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));

            _clientGameManager.GameStarted += OnGameStarted;
        }

        private void OnGameStarted(object sender, GameData gameData)
        {
            _hand.Clear();
            foreach (var card in gameData.IsControllingCurrentPlayer ? gameData.CurrentPlayer.HandCards : gameData.NextPlayer.HandCards)
                _hand.Add(card);
        }
    }
}
