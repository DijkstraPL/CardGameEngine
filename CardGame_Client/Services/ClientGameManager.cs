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
        public GameData GameData { get; private set; }

        private readonly IConnectionManager _connectionManager;

        public event EventHandler<GameData> GameStarted;
        public event EventHandler<GameData> CardTaken;
        public event EventHandler<GameData> TurnStarted;
        public event EventHandler<GameData> CardPlayed;

        public ClientGameManager(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));

            _connectionManager.Connection.On<GameData>("GameStarted", (game) =>
            {
                GameData = game;
                GameStarted?.Invoke(this, game);
            });

            _connectionManager.Connection.On<GameData>("LandCardTaken", (game) =>
            {
                GameData = game;
                CardTaken?.Invoke(this, game);
            });
            _connectionManager.Connection.On<GameData>("CardTaken", (game) =>
            {
                GameData = game;
                CardTaken?.Invoke(this, game);
            });
            _connectionManager.Connection.On<GameData>("TurnStarted", (game) =>
            {
                GameData = game;
                TurnStarted?.Invoke(this, game);
            });
            _connectionManager.Connection.On<GameData>("CardPlayed", (game) =>
            {
                GameData = game;
                CardPlayed?.Invoke(this, game);
            });
        }

        public async Task SetReady(string playerName, string deckName)
        {
            await _connectionManager.Connection.SendAsync("SetReady", playerName, deckName);
        }

        public async Task FinishTurn()
        {
            await _connectionManager.Connection.SendAsync("FinishTurn");
        }

        public async Task DrawLandCard()
        {
            await _connectionManager.Connection.SendAsync("DrawLandCard");
        }

        public async Task DrawCard()
        {
            await _connectionManager.Connection.SendAsync("DrawCard");
        }

        public async Task PlayCard(CardData cardData, SelectionTargetData selectionTargetData)
        {
            await _connectionManager.Connection.SendAsync("PlayCard", cardData, selectionTargetData);
        }

        public async Task SetAttackTarget(CardData attackSource, CardData attackTarget)
        {
            await _connectionManager.Connection.SendAsync("SetAttackTarget", attackSource, attackTarget);
        }
    }
}
