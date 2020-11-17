using CardGame_Game.Game;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players.Interfaces;
using CardGame_Server.Hubs;
using CardGame_Server.Mappers.Interfaces;
using CardGame_Server.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Models
{
    public class ConnectedGroup
    {
        public string Name { get; }
        public ConnectedPlayer Player1 { get; set; }
        public ConnectedPlayer Player2 { get; set; }

        public Game Game => _gameManager.Game;

        public bool ReadyToPlay => Player1 != null && Player2 != null &&
            Player1.Status == Status.ReadyToPlay && Player2.Status == Status.ReadyToPlay;

        public readonly object TurnFinishingLock = new object();

        private IGameEventsContainer _gameEventsContainer;
        private GameManager _gameManager;

        private readonly IHubContext<GameHub> _hubContext;
        private readonly IMapper _mapper;

        private bool _gameFinished = false;

        public ConnectedGroup(string name, IHubContext<GameHub> hubContext, IMapper mapper)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Init()
        {
            _gameEventsContainer = new GameEventsContainer();
            _gameManager = new GameManager(_gameEventsContainer);

            await Player1.InitPlayer(_gameEventsContainer);
            await Player2.InitPlayer(_gameEventsContainer);

            _gameManager.GameInit(Player1.Player, Player2.Player);
            _gameManager.StartGame();
            Player1.Status = Status.InGame;
            Player2.Status = Status.InGame;

            _gameManager.Game.TurnTimer.Timer.Elapsed += Tick;
            _gameManager.Game.TurnTimer.TimeEnded += TimeEnded;
            _gameManager.Game.GameEventsContainer.GameFinishedEvent.Add(null, async gea => await OnGameFinished());
        }

        public bool IsCurrentPlayer(ConnectedPlayer player)
            => _gameManager.Game.CurrentPlayer == player.Player;

        private void TimeEnded(object sender, EventArgs e)
        {
            lock (TurnFinishingLock)
            {
                ForceFinishTurn();
            }
        }

        private void Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            _hubContext.Clients.Group(Name).SendAsync("TimeTick", _gameManager.Game.CurrentPlayer.Name + ": " + _gameManager.Game.TurnTimer.Time.ToString());
        }

        private async Task ForceFinishTurn()
        {
            _gameManager.Game.FinishTurn();

            await _hubContext.Clients.Group(Name).SendAsync("RegisterServerMessage", $"Turn {_gameManager.Game.TurnCounter} started");
            await _hubContext.Clients.Client(Player1.ConnectionId)
                .SendAsync("TurnStarted", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: IsCurrentPlayer(Player1)));
            await _hubContext.Clients.Client(Player2.ConnectionId)
                .SendAsync("TurnStarted", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: IsCurrentPlayer(Player2)));
        }

        private async Task OnGameFinished()
        {
            if (_gameManager.Game.IsGameFinished() && !_gameFinished)
            {
                _gameFinished = true;
                var players = new List<IPlayer> { _gameManager.Game.CurrentPlayer, _gameManager.Game.NextPlayer };
                var winner = players.SingleOrDefault(p => !p.IsLoser);
                var loser = players.SingleOrDefault(p => p.IsLoser);
                if (winner != null)
                    await _hubContext.Clients.Group(Name).SendAsync("RegisterServerMessage", $"{winner.Name} won the game.");
                else
                    await _hubContext.Clients.Group(Name).SendAsync("RegisterServerMessage", $"Somehow we have a draw.");

                await _hubContext.Clients.Client(Player1.ConnectionId)
                    .SendAsync("GameFinished", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: IsCurrentPlayer(Player1)));
                await _hubContext.Clients.Client(Player2.ConnectionId)
                    .SendAsync("GameFinished", _mapper.MapGame(_gameManager.Game, isCurrentPlayer: IsCurrentPlayer(Player2)));
            }
        }

    }
}
