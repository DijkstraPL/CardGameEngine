using CardGame_Client.Data;
using CardGame_Client.Services;
using CardGame_Client.Services.Interfaces;
using CardGame_Client.Views;
using CardGame_Data.GameData;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardGame_Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IList<string> _messages = new ObservableCollection<string>();
        public IEnumerable<string> Messages => _messages;

        private readonly IContainerProvider _containerProvider;
        private readonly IClientGameManager _clientGameManager;
        private readonly IConnectionManager _connectionManager;
        private readonly ITargetSelectionManagement _targetSelectionManagement;

        public MainWindowViewModel(IContainerProvider containerProvider, IClientGameManager clientGameManager, IConnectionManager connectionManager)
        {
            _containerProvider = containerProvider ?? throw new ArgumentNullException(nameof(containerProvider));
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));

            _clientGameManager.GameStarted += OnGameStarted;

            _connectionManager.NewMessageAppeared += (s, e) =>
            {
                if (_messages.Count > 5)
                    _messages.RemoveAt(0);
                _messages.Add(e);
            };
        }

        private void OnGameStarted(object sender, GameData game)
        {
            var regionManager = _containerProvider.Resolve<IRegionManager>();
            regionManager.RequestNavigate(RegionNames.Main, nameof(GameView));
        }
    }
}
