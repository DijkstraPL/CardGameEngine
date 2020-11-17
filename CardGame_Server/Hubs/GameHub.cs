using CardGame_Data.GameData;
using CardGame_DataAccess.Repositories.Interfaces;
using CardGame_Game.BoardTable;
using CardGame_Game.Cards;
using CardGame_Server.Mappers.Interfaces;
using CardGame_Server.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Hubs
{

    public class GameHub : Hub
    {
        public static IList<ConnectedPlayer> ConnectedPlayers { get; } = new List<ConnectedPlayer>();
        public static IList<ConnectedGroup> ConnectedGroups { get; } = new List<ConnectedGroup>();

        private readonly IServiceProvider _serviceProvider;

        private static readonly object _pendingConnectionsLock = new object();
        private readonly IDeckRepository _deckRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHub> _hubContext;

        public GameHub(IServiceProvider serviceProvider, IDeckRepository deckRepository, IMapper mapper, IHubContext<GameHub> hubContext)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _deckRepository = deckRepository ?? throw new ArgumentNullException(nameof(deckRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public override async Task OnConnectedAsync()
        {
            var newPlayer = new ConnectedPlayer(Context.ConnectionId, Status.Connected);
            lock (_pendingConnectionsLock)
                ConnectedPlayers.Add(newPlayer);

            newPlayer.Decks = await _deckRepository.GetDecks();
           
            await Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            lock (_pendingConnectionsLock)
            {
                ConnectedPlayers.Remove(ConnectedPlayers.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId));
                //ConnectedIds.Remove(Context.ConnectionId);
                //var playerData = Players.FirstOrDefault(p => p.connectionId == Context.ConnectionId);
                //if (playerData != default)
                //    Players.Remove(playerData);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SetReady(string playerName, string deckName)
        {
            var connectedPlayer = ConnectedPlayers.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
            if (connectedPlayer == null)
                throw new ArgumentException();

            lock (_pendingConnectionsLock)
            {
                connectedPlayer.Status = Status.ReadyToPlay;             
                connectedPlayer.SetPlayer(playerName, deckName);
            }

            await Clients.Caller.SendAsync("RegisterServerMessage", playerName + " is ready");
            await Clients.Caller.SendAsync("RegisterServerMessage", "Looking for player");

            lock (_pendingConnectionsLock)
            {
                var groupForPlay = ConnectedGroups.FirstOrDefault(cg => cg.Player1 != null && cg.Player2 == null || cg.Player1 == null && cg.Player2 != null);

                if (groupForPlay != null)
                {
                    if (groupForPlay.Player1 == null)
                        groupForPlay.Player1 = connectedPlayer;
                    else if (groupForPlay.Player2 == null)
                        groupForPlay.Player2 = connectedPlayer;
                    connectedPlayer.Group = groupForPlay;

                    Groups.AddToGroupAsync(Context.ConnectionId, groupForPlay.Name);

                    if (groupForPlay.ReadyToPlay)
                        StartGame(groupForPlay);
                }
                else
                {
                    string groupName = Guid.NewGuid().ToString();
                    var connectedGroup = new ConnectedGroup(groupName, _hubContext, _mapper);
                    ConnectedGroups.Add(connectedGroup);

                    if (connectedGroup.Player1 == null)
                        connectedGroup.Player1 = connectedPlayer;
                    else if (connectedGroup.Player2 == null)
                        connectedGroup.Player2 = connectedPlayer;
                    connectedPlayer.Group = connectedGroup;

                    Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                    Clients.Group(groupName).SendAsync("RegisterServerMessage", "Waiting for other player");
                }
            }
        }

        private async Task StartGame(ConnectedGroup group)
        {
            await group.Init();
            group.IsCurrentPlayer(group.Player1);

            await Clients.Client(group.Player1.ConnectionId)
                .SendAsync("GameStarted", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player1)));
            await Clients.Client(group.Player2.ConnectionId)
                .SendAsync("GameStarted", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player2)));

            await Clients.Group(group.Name).SendAsync("RegisterServerMessage", $"Game started");
            await Clients.Group(group.Name).SendAsync("RegisterServerMessage", $"Turn {group.Game.TurnCounter} started");
            await Clients.Group(group.Name).SendAsync("RegisterServerMessage", group.Game.CurrentPlayer.Name + " has first move");
        }

        private ConnectedGroup FindGroup(string connectionId)
            => ConnectedGroups.FirstOrDefault(p => p.Player1?.ConnectionId == connectionId || p.Player2?.ConnectionId == connectionId);

        private ConnectedPlayer FindPlayer(ConnectedGroup group, string connectionId)
        {
            if (group.Player1.ConnectionId == connectionId)
                return group.Player1;
            if (group.Player2.ConnectionId == connectionId)
                return group.Player2;
            throw new ArgumentException();
        }

        public async Task DrawLandCard()
        {
            var group = FindGroup(Context.ConnectionId);
            var invocationPlayer = FindPlayer(group, Context.ConnectionId);
            if (!group.IsCurrentPlayer(invocationPlayer))
                return;
            if (group.Game.GetCardFromLandDeck())
            {
                await Clients.Group(group.Name).SendAsync("RegisterServerMessage", group.Game.CurrentPlayer.Name + " took land card");
                await Clients.Client(group.Player1.ConnectionId)
                    .SendAsync("LandCardTaken", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player1)));
                await Clients.Client(group.Player2.ConnectionId)
                    .SendAsync("LandCardTaken", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player2)));
            }
            else
                await Clients.Caller.SendAsync("RegisterServerMessage", "Can't take a card");
        }

        public async Task DrawCard()
        {
            var group = FindGroup(Context.ConnectionId);
            var invocationPlayer = FindPlayer(group, Context.ConnectionId);
            if (!group.IsCurrentPlayer(invocationPlayer))
                return;
            if (group.Game.GetCardFromDeck())
            {
                await Clients.Group(group.Name).SendAsync("RegisterServerMessage", group.Game.CurrentPlayer.Name + " took card");
                await Clients.Client(group.Player1.ConnectionId)
                    .SendAsync("CardTaken", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player1)));
                await Clients.Client(group.Player2.ConnectionId)
                    .SendAsync("CardTaken", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player2)));
            }
            else
                await Clients.Caller.SendAsync("RegisterServerMessage", "Can't take a card");
        }
        public async Task FinishTurn()
        {
            var group = FindGroup(Context.ConnectionId);
            var invocationPlayer = FindPlayer(group, Context.ConnectionId);;

            lock (group.TurnFinishingLock)
            {
                if (!group.IsCurrentPlayer(invocationPlayer))
                    return;

                group.Game.FinishTurn();

                Clients.Group(group.Name).SendAsync("RegisterServerMessage", $"Turn {group.Game.TurnCounter} started");
                Clients.Client(group.Player1.ConnectionId)
                    .SendAsync("TurnStarted", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player1)));
                Clients.Client(group.Player2.ConnectionId)
                    .SendAsync("TurnStarted", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player2)));
                Clients.Group(group.Name).SendAsync("RegisterServerMessage", group.Game.CurrentPlayer.Name + " turn");
            }
        }

        public async Task PlayCard(CardData cardData, SelectionTargetData selectionTarget)
        {
            var group = FindGroup(Context.ConnectionId);
            var invocationPlayer = FindPlayer(group, Context.ConnectionId);
            if (!group.IsCurrentPlayer(invocationPlayer))
                return;

            var card = group.Game.CurrentPlayer.Hand.FirstOrDefault(c => c.Identifier == cardData.Identifier);
            var enemyFields = group.Game.NextPlayer.BoardSide.Fields;
            var currentPlayerFields = group.Game.CurrentPlayer.BoardSide.Fields;
            Field field = null;
            if (selectionTarget?.TargetEnemyField != null)
                field = enemyFields.FirstOrDefault(f =>
               f.X == selectionTarget.TargetEnemyField.X &&
               f.Y == selectionTarget.TargetEnemyField.Y);
            if (selectionTarget?.TargetOwnField != null)
                field = currentPlayerFields.FirstOrDefault(f =>
               f.X == selectionTarget.TargetOwnField.X &&
               f.Y == selectionTarget.TargetOwnField.Y);

            if (group.Game.PlayCard(card, new InvocationData { Field = field }))
            {
                await Clients.Group(group.Name).SendAsync("RegisterServerMessage", group.Game.CurrentPlayer.Name + " play " + cardData.Name);

                await Clients.Client(group.Player1.ConnectionId)
                    .SendAsync("CardPlayed", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player1)));
                await Clients.Client(group.Player2.ConnectionId)
                    .SendAsync("CardPlayed", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player2)));
            }
            else
                await Clients.Caller.SendAsync("RegisterServerMessage", "Can't play a card");
        }

        public async Task SetAttackTarget(CardData sourceCardData, CardData targetCardData)
        {
            var group = FindGroup(Context.ConnectionId);
            var invocationPlayer = FindPlayer(group, Context.ConnectionId);
            if (!group.IsCurrentPlayer(invocationPlayer))
                return;

            var sourceField = group.Game.CurrentPlayer.BoardSide.Fields.FirstOrDefault(f => f.Card?.Identifier == sourceCardData.Identifier);
            var targetField = group.Game.NextPlayer.BoardSide.Fields.FirstOrDefault(f => f.Card?.Identifier == targetCardData.Identifier);
            if (sourceField != null && targetField != null)
            {
                sourceField.Card.SetAttackTarget(targetField.Card);

                await Clients.Client(group.Player1.ConnectionId)
                    .SendAsync("AttackTargetSet", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player1)));
                await Clients.Client(group.Player2.ConnectionId)
                    .SendAsync("AttackTargetSet", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player2)));
            }
        }

        public async Task SetPlayerAsAttackTarget(CardData sourceCardData, PlayerData targetPlayerData)
        {
            var group = FindGroup(Context.ConnectionId);
            var invocationPlayer = FindPlayer(group, Context.ConnectionId);
            if (!group.IsCurrentPlayer(invocationPlayer))
                return;

            var sourceField = group.Game.CurrentPlayer.BoardSide.Fields.FirstOrDefault(f => f.Card?.Identifier == sourceCardData.Identifier);
            var targetPlayer = group.Game.NextPlayer;

            if (sourceField != null && targetPlayer.Name == targetPlayerData?.Name)
            {
                sourceField.Card.SetAttackTarget(targetPlayer);

                await Clients.Client(group.Player1.ConnectionId)
                    .SendAsync("AttackTargetSet", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player1)));
                await Clients.Client(group.Player2.ConnectionId)
                    .SendAsync("AttackTargetSet", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player2)));
            }
        }

        public async Task Move(CardData sourceCardData, FieldData fieldData)
        {
            var group = FindGroup(Context.ConnectionId);
            var invocationPlayer = FindPlayer(group, Context.ConnectionId);
            if (!group.IsCurrentPlayer(invocationPlayer))
                return;


            var sourceField = group.Game.CurrentPlayer.BoardSide.Fields.FirstOrDefault(f => f.Card?.Identifier == sourceCardData.Identifier);
            var targetField = group.Game.CurrentPlayer.BoardSide.Fields.FirstOrDefault(f => f.X == fieldData.X && f.Y == fieldData.Y);
            if (sourceField != null && targetField != null)
            {
                group.Game.CurrentPlayer.BoardSide.Move(group.Game.CurrentPlayer, sourceField, targetField);

                await Clients.Client(group.Player1.ConnectionId)
                    .SendAsync("CardMoved", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player1)));
                await Clients.Client(group.Player2.ConnectionId)
                    .SendAsync("CardMoved", _mapper.MapGame(group.Game, isCurrentPlayer: group.IsCurrentPlayer(group.Player2)));
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }



    }
}
