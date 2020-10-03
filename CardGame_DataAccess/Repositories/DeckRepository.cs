using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardGame_DataAccess.Repositories
{
    public class DeckRepository : IDisposable
    {
        private readonly CardGameDbContext _cardGameDbContext;

        public DeckRepository(CardGameDbContext cardGameDbContext)
        {
            _cardGameDbContext = cardGameDbContext ?? throw new ArgumentNullException(nameof(cardGameDbContext));
        }

        public async Task CreateDeck(Deck deck)
        {
            await _cardGameDbContext.Decks.AddAsync(deck);
            await _cardGameDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Deck>> GetDecks()
        {
            return await _cardGameDbContext.Decks
                .Include(d => d.Cards)
                .ThenInclude(cd => cd.Card)
                .Include(d => d.Cards)
                .ThenInclude(cd => cd.Card.Set)
                .Include(d => d.Cards)
                .ThenInclude(cd => cd.Card.CardType)
                .Include(d => d.Cards)
                .ThenInclude(cd => cd.Card.SubType)
                .Include(d => d.Cards)
                .ThenInclude(cd => cd.Card.Rules)
                .ThenInclude(cr => cr.Rule)
                 .ToListAsync();
        }

        public void Dispose()
        {
            _cardGameDbContext.Dispose();
        }
    }
}
