using CardGame_Data.Data;
using CardGame_DataAccess.Repositories.Interfaces;
using CardGame_Game.BoardTable;
using CardGame_Game.BoardTable.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.Game;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using CardGame_Game.Players;
using CardGame_Game.Players.Interfaces;
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
        private static IGameEventsContainer _gameEventsContainer;

        public GameHub(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository ?? throw new ArgumentNullException(nameof(deckRepository));
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
                ConnectedIds.Remove(Context.ConnectionId);
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

            if (ConnectedIds.All(c => c.Value == Status.ReadyToPlay))
                await StartGame();
        }

        public async Task StartGame()
        {
            if (ConnectedIds.All(c => c.Value == Status.ReadyToPlay))
            {
                var gameManager = new GameManager(_gameEventsContainer);
                gameManager.GameInit(Players.First().player, Players.Last().player);
                gameManager.StartGame();
                await Clients.All.SendAsync("GameStarted", gameManager.Game);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    public class PlayerManager
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IGameEventsContainer _gameEventsContainer;

        public PlayerManager(IDeckRepository deckRepository, IGameEventsContainer gameEventsContainer)
        {
            _deckRepository = deckRepository ?? throw new ArgumentNullException(nameof(deckRepository));
            _gameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));
        }

        public async Task<IPlayer> GetPlayer(string playerName, string deckName)
        {
            var decks = await _deckRepository.GetDecks();
            var deck = decks.FirstOrDefault(d => d.Name == deckName);

            var landDeck = new Stack<Card>();
            var landCards = deck.Cards.Where(cd => cd.Card.Kind == CardGame_DataAccess.Entities.Enums.Kind.Land);
            foreach (var landCard in landCards)
                for (int i = 0; i < landCard.Amount; i++)
                    landDeck.Push(landCard.Card);
            var cardDeck = new Stack<Card>();
            var cards = deck.Cards.Where(cd => cd.Card.Kind != CardGame_DataAccess.Entities.Enums.Kind.Land);
            foreach (var card in cards)
                for (int i = 0; i < card.Amount; i++)
                    cardDeck.Push(card.Card);

            return new BluePlayer(playerName, cardDeck, landDeck, new GameCardFactory(), _gameEventsContainer);
        }
    }

    public class GameManager
    {
        private IPlayer _firstPlayer;
        private IPlayer _secondPlayer;
        private IGameEventsContainer _gameEventsContainer;
        private IBoard _board;
        public Game Game { get; private set; }

        public GameManager(IGameEventsContainer gameEventsContainer)
        {
            _gameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));
        }

        public void GameInit(IPlayer firstPlayer, IPlayer secondPlayer)
        {
            _firstPlayer = firstPlayer ?? throw new ArgumentNullException(nameof(firstPlayer));
            _secondPlayer = secondPlayer ?? throw new ArgumentNullException(nameof(secondPlayer));

            _board = new Board(_gameEventsContainer);

            Game = new Game(_firstPlayer, _secondPlayer, _board, new RandomHelper(), _gameEventsContainer);
        }

        public void StartGame()
        {
            Game.StartGame();
        }
    }
}
