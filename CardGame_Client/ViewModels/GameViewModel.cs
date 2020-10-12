using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CardGame_Client.ViewModels
{
    public class GameViewModel:BindableBase
    {
        private readonly IList<CardData> _hand = new ObservableCollection<CardData>();
        public IEnumerable<CardData> Hand => _hand;

        private IList<LineViewModel> _lines = new ObservableCollection<LineViewModel>();
        public IEnumerable<LineViewModel> Lines => _lines;

        private readonly IClientGameManager _clientGameManager;
        private readonly ITargetSelectionManagement _targetSelectionManagement;

        public GameViewModel(IClientGameManager clientGameManager, ITargetSelectionManagement targetSelectionManagement)
        {
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _targetSelectionManagement = targetSelectionManagement ?? throw new ArgumentNullException(nameof(targetSelectionManagement));
            _clientGameManager.GameStarted += OnGameStarted;

            _targetSelectionManagement.SetGameViewModel(this);
        }

        internal void ClearLines()
        {
            _lines.Clear();
        }

        internal void SetLine(BoardFieldViewModel source, LineViewModel lineViewModel)
        {
            _lines.Add(lineViewModel);
            RaisePropertyChanged(nameof(Lines));
        }

        private void OnGameStarted(object sender, GameData gameData)
        {
            _hand.Clear();
            foreach (var card in gameData.IsControllingCurrentPlayer ? gameData.CurrentPlayer.HandCards : gameData.NextPlayer.HandCards)
                _hand.Add(card);
        }
    }
}
