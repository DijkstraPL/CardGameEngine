using CardGame_Client.Services;
using CardGame_Client.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardGame_Client.ViewModels
{
    public class WaitingLobbyViewModel : BindableBase
    {
        public ICommand ConnectCommand { get; }

        private string _connectionStatus;
        public string ConnectionStatus
        {
            get => _connectionStatus;
            set => SetProperty(ref _connectionStatus, value);
        }

        private IList<string> _messages = new ObservableCollection<string>();
        public IEnumerable<string> Messages => _messages;

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                SetProperty(ref _isConnected, value);
                if (IsConnected)
                    GetDecks();
            }
        }

        public string PlayerName { get; set; }
        private string _selectedDeck;
        public string SelectedDeck
        {
            get => _selectedDeck;
            set => SetProperty(ref _selectedDeck, value);
        }

        private IList<string> _decks = new ObservableCollection<string>();
        public IEnumerable<string> Decks => _decks;

        public ICommand StartGameCommand { get; }

        private readonly IConnectionManager _connectionManager;
        private readonly IDecksProvider _decksProvider;
        private readonly IClientGameManager _clientGameManager;

        public WaitingLobbyViewModel(IConnectionManager connectionManager, IDecksProvider decksProvider, IClientGameManager clientGameManager)
        {
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
            _decksProvider = decksProvider ?? throw new ArgumentNullException(nameof(decksProvider));
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));

            ConnectionStatus = _connectionManager.ConnectionStatus;
            _connectionManager.ConnectionStatusChanged += (s, e) =>
            {
                ConnectionStatus = _connectionManager.ConnectionStatus;
                IsConnected = _connectionManager.Connection.State == HubConnectionState.Connected;
            };
            _connectionManager.NewMessageAppeared += (s, e) =>
            {
                _messages.Add(e);
            };

            ConnectCommand = new DelegateCommand(() => _connectionManager.Connect());
            StartGameCommand = new DelegateCommand(() => _clientGameManager.SetReady(PlayerName, SelectedDeck));
        }


        private async Task GetDecks()
        {
            _decks.Clear();
            foreach (var deckName in await _decksProvider.GetDecks())
                _decks.Add(deckName);
            SelectedDeck = _decks.FirstOrDefault();
        }
    }
}
