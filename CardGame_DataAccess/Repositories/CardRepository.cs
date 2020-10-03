using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_DataAccess.Repositories
{
    public class CardRepository : IDisposable
    {
        private readonly CardGameDbContext _cardGameDbContext;

        public CardRepository(CardGameDbContext cardGameDbContext)
        {
            _cardGameDbContext = cardGameDbContext ?? throw new ArgumentNullException(nameof(cardGameDbContext));
        }

        public async Task CreateCard(Card card)
        {
            await _cardGameDbContext.Cards.AddAsync(card);
            await _cardGameDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> GetCards()
        {
           return  await _cardGameDbContext.Cards
                .Include(c => c.Set)
                .Include(c => c.CardType)
                .Include(c => c.SubType)
                .Include(c => c.Rules)
                .ThenInclude(r => r.Rule)
                .ToListAsync();
        }

        public void Dispose()
        {
            _cardGameDbContext.Dispose();
        }
    }
}
