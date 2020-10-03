using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_DataAccess.Repositories
{
    public class SetRepository : IDisposable
    {
        private readonly CardGameDbContext _cardGameDbContext;

        public SetRepository(CardGameDbContext cardGameDbContext)
        {
            _cardGameDbContext = cardGameDbContext ?? throw new ArgumentNullException(nameof(cardGameDbContext));
        }

        public async Task CreateSet(Set set)
        {
            await _cardGameDbContext.Sets.AddAsync(set);
            await _cardGameDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Set>> GetSets()
        {
            return await _cardGameDbContext.Sets.ToListAsync();
        }

        public void Dispose()
        {
            _cardGameDbContext.Dispose();
        }

        public async Task<Set> GetSetWithName(string name)
        {
            return await _cardGameDbContext.Sets.FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
