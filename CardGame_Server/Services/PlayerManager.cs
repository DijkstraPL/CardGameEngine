using CardGame_Data.Data;
using CardGame_DataAccess.Repositories.Interfaces;
using CardGame_Game.Cards;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players;
using CardGame_Game.Players.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Services
{
    public class PlayerManager
    {
        //private readonly IDeckRepository _deckRepository;
        private readonly IGameEventsContainer _gameEventsContainer;
        private IEnumerable<CardGame_DataAccess.Entities.Deck> _decks;

        //public PlayerManager(IDeckRepository deckRepository, IGameEventsContainer gameEventsContainer)
        //{
        //    _deckRepository = deckRepository ?? throw new ArgumentNullException(nameof(deckRepository));
        //    _gameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));
        //}

        public PlayerManager(IEnumerable<CardGame_DataAccess.Entities.Deck> decks, IGameEventsContainer gameEventsContainer)
        {
            _decks = decks;
            _gameEventsContainer = gameEventsContainer;
        }

        public async Task<IPlayer> GetPlayer(string playerName, string deckName)
        {
            var deck = _decks.FirstOrDefault(d => d.Name == deckName);

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
}
