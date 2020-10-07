using CardGame_DataAccess.Repositories.Interfaces;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players.Interfaces;
using CardGame_Server.Mappers.Interfaces;
using CardGame_Server.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Hubs
{
    public enum Status
    {
        Connected,
        ReadyToPlay
    }

    public class GameHub : Hub
    {
        public static Dictionary<string, Status> ConnectedIds { get; } = new Dictionary<string, Status>();
        public static HashSet<(string connectionId, IPlayer player)> Players { get; } = new HashSet<(string connectionId, IPlayer player)>();
        private static readonly object _pendingConnectionsLock = new object();
        private static IDeckRepository _deckRepository;
        private readonly IMapper _mapper;
        private static IGameEventsContainer _gameEventsContainer;
        private static GameManager _gameManager;

        public GameHub(IDeckRepository deckRepository, IMapper mapper)
        {
            _deckRepository = deckRepository ?? throw new ArgumentNullException(nameof(deckRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _gameEventsContainer = new GameEventsContainer();
        }

        public override Task OnConnectedAsync()
        {
            lock (_pendingConnectionsLock)
                ConnectedIds.Add(Context.ConnectionId, Status.Connected);

            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            lock (_pendingConnectionsLock)
            {
                ConnectedIds.Remove(Context.ConnectionId);
                var playerData = Players.FirstOrDefault(p => p.connectionId == Context.ConnectionId);
                if (playerData != default)
                    Players.Remove(playerData);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SetReady(string playerName, string deckName)
        {
            if (!ConnectedIds.ContainsKey(Context.ConnectionId))
                throw new ArgumentException();
            var playerManager = new PlayerManager(_deckRepository, _gameEventsContainer);
            var player = await playerManager.GetPlayer(playerName, deckName);
            lock (_pendingConnectionsLock)
            {
                Players.Add((Context.ConnectionId, player));
                ConnectedIds[Context.ConnectionId] = Status.ReadyToPlay;
            }

            await Clients.All.SendAsync("RegisterServerMessage", playerName + " is ready");

            if (ConnectedIds.Count % 2 == 0 && ConnectedIds.All(c => c.Value == Status.ReadyToPlay))
                await StartGame();
            else
                await Clients.All.SendAsync("RegisterServerMessage", "Waiting for other player");
        }

        public async Task DrawLandCard()
        {
            var invocationPlayer = Players.First(p => p.connectionId == Context.ConnectionId);
            if (invocationPlayer.player != _gameManager.Game.CurrentPlayer)
                return;
            if (_gameManager.Game.GetCardFromLandDeck())
            {
                await Clients.All.SendAsync("RegisterServerMessage", _gameManager.Game.CurrentPlayer.Name + " took land card");
                await Clients.Client(Players.First().connectionId)
                    .SendAsync("LandCardTaken", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: _gameManager.Game.CurrentPlayer == Players.First().player));
                await Clients.Client(Players.Last().connectionId)
                    .SendAsync("LandCardTaken", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: _gameManager.Game.CurrentPlayer == Players.Last().player));
            }
            else
                await Clients.Caller.SendAsync("RegisterServerMessage", "Can't take a card");
        }

        public async Task DrawCard()
        {
            var invocationPlayer = Players.First(p => p.connectionId == Context.ConnectionId);
            if (invocationPlayer.player != _gameManager.Game.CurrentPlayer)
                return;
            if (_gameManager.Game.GetCardFromDeck())
            {
                await Clients.All.SendAsync("RegisterServerMessage", _gameManager.Game.CurrentPlayer.Name + " took card");
                await Clients.Client(Players.First().connectionId)
                    .SendAsync("CardTaken", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: _gameManager.Game.CurrentPlayer == Players.First().player));
                await Clients.Client(Players.Last().connectionId)
                    .SendAsync("CardTaken", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: _gameManager.Game.CurrentPlayer == Players.Last().player));
            }
            else
                await Clients.Caller.SendAsync("RegisterServerMessage", "Can't take a card");
        }
        public async Task FinishTurn()
        {
            var invocationPlayer = Players.First(p => p.connectionId == Context.ConnectionId);
            if (invocationPlayer.player != _gameManager.Game.CurrentPlayer)
                return;
            _gameManager.Game.FinishTurn();

            await Clients.All.SendAsync("RegisterServerMessage", $"Turn {_gameManager.Game.TurnCounter} started");
            await Clients.Client(Players.First().connectionId)
                .SendAsync("TurnStarted", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: _gameManager.Game.CurrentPlayer == Players.First().player));
            await Clients.Client(Players.Last().connectionId)
                .SendAsync("TurnStarted", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: _gameManager.Game.CurrentPlayer == Players.Last().player));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private async Task StartGame()
        {
            _gameManager = new GameManager(_gameEventsContainer);
            _gameManager.GameInit(Players.First().player, Players.Last().player);
            _gameManager.StartGame();

            await Clients.Client(Players.First().connectionId)
                .SendAsync("GameStarted", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: _gameManager.Game.CurrentPlayer == Players.First().player));
            await Clients.Client(Players.Last().connectionId)
                .SendAsync("GameStarted", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: _gameManager.Game.CurrentPlayer == Players.Last().player));

            await Clients.All.SendAsync("RegisterServerMessage", $"Game started");
            await Clients.All.SendAsync("RegisterServerMessage", $"Turn {_gameManager.Game.TurnCounter} started");
        }
    }
}
