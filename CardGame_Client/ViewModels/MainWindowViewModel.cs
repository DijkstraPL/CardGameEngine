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
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardGame_Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IContainerProvider _containerProvider;
        private readonly IClientGameManager _clientGameManager;

        public MainWindowViewModel(IContainerProvider containerProvider, IClientGameManager clientGameManager)
        {
            _containerProvider = containerProvider ?? throw new ArgumentNullException(nameof(containerProvider));
            _clientGameManager = clientGameManager ?? throw new ArgumentNullException(nameof(clientGameManager));

            _clientGameManager.GameStarted += OnGameStarted;
        }

        private void OnGameStarted(object sender, GameData game)
        {
            var regionManager = _containerProvider.Resolve<IRegionManager>();
            regionManager.RequestNavigate(RegionNames.Main, nameof(GameView));
        }
    }
}
