using CardGame_Client.Services;
using CardGame_Client.Services.Interfaces;
using CardGame_Client.Views;
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
            containerRegistry.RegisterInstance<IContainerProvider>(Container);
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();

            IRegionManager regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Data.RegionNames.Main, typeof(WaitingLobbyView));
            regionManager.RegisterViewWithRegion(Data.RegionNames.Main, typeof(GameView));
        }
    }
}
