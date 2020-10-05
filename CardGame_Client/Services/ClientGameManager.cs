using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_Client.Services
{
    public class ClientGameManager : Service, IClientGameManager
    {
        private readonly IConnectionManager _connectionManager;

        public event EventHandler<GameData> GameStarted;

        public ClientGameManager(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));

            _connectionManager.Connection.On<GameData>("GameStarted", (game) =>
            {
                _connectionManager.AddMessage("Game started");
                GameStarted?.Invoke(this, game);
            });
        }

        public async Task SetReady(string playerName, string deckName)
        {
            await _connectionManager.Connection.SendAsync("SetReady", playerName, deckName);
        }


    }
}
