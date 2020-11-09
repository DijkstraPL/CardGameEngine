using CardGame_DataAccess.Entities;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players.Interfaces;
using CardGame_Server.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardGame_Server.Models
{
    public class ConnectedPlayer
    {
        public string ConnectionId { get; }
        public ConnectedGroup Group { get; set; }
        public Status Status { get; set; }
        public IPlayer Player { get; set; }

        public string PlayerName { get; private set; }
        public string DeckName { get; private set; }
        public IEnumerable<Deck> Decks { get; internal set; }

        private PlayerManager _playerManager;

        public ConnectedPlayer(string connectionId, Status status = Status.Connected)
        {
            ConnectionId = connectionId ?? throw new ArgumentNullException(nameof(connectionId));
            Status = status;
        }

        public void SetPlayer(string playerName, string deckName)
        {
            PlayerName = playerName ?? throw new ArgumentNullException(nameof(playerName));
            DeckName = deckName ?? throw new ArgumentNullException(nameof(deckName));
        }

        public async Task InitPlayer(IGameEventsContainer gameEventsContainer)
        {
            if (gameEventsContainer is null)
                throw new ArgumentNullException(nameof(gameEventsContainer));

            _playerManager = new PlayerManager(Decks, gameEventsContainer);
            Player = await _playerManager.GetPlayer(PlayerName, DeckName);
        }
    }
}
