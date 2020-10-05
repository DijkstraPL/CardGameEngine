using CardGame_Client.Services.Interfaces;
using CardGame_Data.GameData;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_Client.Services
{
    public class ConnectionManager : Service, IConnectionManager
    {
        private string _connectionStatus;
        public string ConnectionStatus
        {
            get => _connectionStatus;
            private set
            {
                _connectionStatus = value;
                ConnectionStatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public HubConnection Connection { get; }

        public event EventHandler ConnectionStatusChanged;
        public event EventHandler<string> NewMessageAppeared;

        public ConnectionManager()
        {
            Connection = new HubConnectionBuilder()
               .WithUrl(Url + "/GameHub")
               .Build();

            Connection.Closed += async (error) =>
            {
                ConnectionStatus = error?.Message;
                await Connection.StartAsync();
            };
            Connection.On<string>("Connected", (connectionid) =>
            {
                ConnectionStatus = "Connected";
            });
            Connection.On<string>("RegisterServerMessage", (message) =>
            {
                NewMessageAppeared?.Invoke(this, message);
            });
        }

        public void AddMessage(string message)
        {
            NewMessageAppeared?.Invoke(this, message);
        }


        public async Task Connect()
        {
            try
            {
                await Connection.StartAsync();
                ConnectionStatus = "Connection started";
            }
            catch (Exception ex)
            {
                ConnectionStatus = ex.Message;
            }

        }
    }
}
