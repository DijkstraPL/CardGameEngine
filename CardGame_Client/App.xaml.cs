using CardGame_Client.Services;
using CardGame_Client.Services.Interfaces;
using CardGame_Client.ViewModels;
using CardGame_Client.Views;
using CardGame_Client.Views.Player;
using CardGame_Client.Views.Enemy;
using CommonServiceLocator;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CardGame_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IConnectionManager, ConnectionManager>();
            containerRegistry.RegisterSingleton<IDecksProvider, DecksProvider>();
            containerRegistry.RegisterSingleton<IClientGameManager, ClientGameManager>();
            containerRegistry.RegisterSingleton<ICardGameManagement, CardGameManagement>();
            containerRegistry.RegisterInstance<IContainerProvider>(Container);
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();

            IRegionManager regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Data.RegionNames.Main, typeof(WaitingLobbyView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.Main, typeof(GameView));

            regionManager.RegisterViewWithRegion(Data.RegionNames.PlayerSide, typeof(PlayerSideView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.PlayerData, typeof(PlayerDataView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.PlayerHand, typeof(PlayerHandView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.PlayerDecks, typeof(PlayerDecksView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.PlayerBoardSide, typeof(PlayerBoardView));

            regionManager.RegisterViewWithRegion(Data.RegionNames.EnemySide, typeof(EnemySideView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.EnemyData, typeof(EnemyDataView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.EnemyHand, typeof(EnemyHandView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.EnemyDecks,typeof(EnemyDecksView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.EnemyBoardSide, typeof(EnemyBoardView));
        }
    }
}
