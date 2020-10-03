using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_DataAccess.Repositories
{
    public class SubTypeRepository : IDisposable
    {
        private readonly CardGameDbContext _cardGameDbContext;

        public SubTypeRepository(CardGameDbContext cardGameDbContext)
        {
            _cardGameDbContext = cardGameDbContext ?? throw new ArgumentNullException(nameof(cardGameDbContext));
        }

        public async Task CreateSubType(Subtype subtype)
        {
            await _cardGameDbContext.Subtypes.AddAsync(subtype);
            await _cardGameDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subtype>> GetSubtypes()
        {
            return await _cardGameDbContext.Subtypes.ToListAsync();
        }

        public async Task<Subtype> GetSubTypeWithNameAsync(string name)
        {
            return await _cardGameDbContext.Subtypes.FirstOrDefaultAsync(s => s.Name == name);
        }

        public void Dispose()
        {
            _cardGameDbContext.Dispose();
        }
    }
}
