using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_DataAccess.Repositories
{
    public class RuleRepository : IDisposable
    {
        private readonly CardGameDbContext _cardGameDbContext;

        public RuleRepository(CardGameDbContext cardGameDbContext)
        {
            _cardGameDbContext = cardGameDbContext ?? throw new ArgumentNullException(nameof(cardGameDbContext));
        }

        public async Task CreateRule(Rule rule)
        {
            await _cardGameDbContext.Rules.AddAsync(rule);
            await _cardGameDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rule>> GetRules()
        {
            return await _cardGameDbContext.Rules.ToListAsync();
        }

        public void Dispose()
        {
            _cardGameDbContext.Dispose();
        }
    }
}
