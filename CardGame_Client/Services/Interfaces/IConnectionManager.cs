using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CardGame_Client.Services.Interfaces
{
    public interface IConnectionManager
    {
        HubConnection Connection { get; }
        string ConnectionStatus { get; }

        event EventHandler ConnectionStatusChanged;
        event EventHandler<string> NewMessageAppeared;

        Task Connect();
    }
}