using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_DataAccess.Repositories
{
    public class CardTypeRepository : IDisposable
    {
        private readonly CardGameDbContext _cardGameDbContext;

        public CardTypeRepository(CardGameDbContext cardGameDbContext)
        {
            _cardGameDbContext = cardGameDbContext ?? throw new ArgumentNullException(nameof(cardGameDbContext));
        }

        public async Task CreateCardTypeAsync(CardType cardType)
        {
            await _cardGameDbContext.CardTypes.AddAsync(cardType);
            await _cardGameDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CardType>> GetCardTypesAsync()
        {
            return await _cardGameDbContext.CardTypes.ToListAsync();
        }

        public async Task<CardType> GetCardTypeWithNameAsync(string name)
        {
            return await _cardGameDbContext.CardTypes.FirstOrDefaultAsync(ct => ct.Name == name);
        }

        public void Dispose()
        {
            _cardGameDbContext.Dispose();
        }
    }
}
