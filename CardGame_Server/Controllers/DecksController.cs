using CardGame_DataAccess.Entities;
using CardGame_DataAccess.Repositories.Interfaces;
using CardGame_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Controllers
{
    [Route("api/decks")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private readonly IDeckRepository _deckRepository;
        private readonly ICardRepository _cardRepository;

        public DecksController(IDeckRepository deckRepository, ICardRepository cardRepository)
        {
            _deckRepository = deckRepository ?? throw new ArgumentNullException(nameof(deckRepository));
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetNames()
        {
            var decks = await _deckRepository.GetDecks();
            return decks.Select(d => d.Name);
        }

        [HttpPost]
        public async Task CreateDeck([FromBody] CardGame_Data.Data.Deck deckResource)
        {
            if (deckResource is null)
                throw new ArgumentException();

            var oldDeck = await _deckRepository.GetDeck(deckResource.Name);
            if (oldDeck != null)
                await RemoveDeck(oldDeck.Name);

            var deck = new Deck();
            deck.Name = deckResource.Name;
            deck.Cards = new List<CardDeck>();

            foreach (var cardResource in deckResource.Cards)
            {
                var card = await _cardRepository.GetCard(cardResource.CardName);
                var cardDeck = new CardDeck
                {
                    CardId = card.Id,
                    Deck = deck,
                    Card = card,
                    Amount = cardResource.Amount
                };
                deck.Cards.Add(cardDeck);
            }

            await _deckRepository.CreateDeck(deck);
        }

        [HttpGet("{id}")]
        public async Task<CardGame_Data.Data.Deck> GetDeck(int id)
        {
            var deck = await _deckRepository.GetDeck(id);
            if (deck is null)
                return null;

            var deckResource = new CardGame_Data.Data.Deck();
            deckResource.Name = deck.Name;
            foreach (var card in deck.Cards)
            {
                for (int i = 0; i < card.Amount; i++)
                    deckResource.Cards.Add(new CardGame_Data.Data.DeckCard
                    { 
                        CardName = card.Card.Name,
                        Amount = card.Amount
                    });
            }

            return deckResource;
        }

        [HttpGet("byname/{deckName}")]
        public async Task<CardGame_Data.Data.Deck> GetDeck(string deckName)
        {
            var deck = await _deckRepository.GetDeck(deckName);
            if (deck is null)
                return null;

            var deckResource = new CardGame_Data.Data.Deck();
            deckResource.Name = deck.Name;
            foreach (var card in deck.Cards)
            {
                for (int i = 0; i < card.Amount; i++)
                    deckResource.Cards.Add(new CardGame_Data.Data.DeckCard
                    {
                        CardName = card.Card.Name,
                        Amount = card.Amount
                    });
            }

            return deckResource;
        }

        [HttpDelete("{deckName}")]
        public async Task RemoveDeck(string deckName)
        {
            await _deckRepository.RemoveDeck(deckName);
        }
    }
}
