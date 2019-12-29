using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_DataAccess.Factories
{
    public class CardGameDbContextFactory : IDesignTimeDbContextFactory<CardGameDbContext>
    {
        public CardGameDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CardGameDbContext>();
            optionsBuilder.UseSqlServer("server=localhost; database=CardGame; Integrated Security=SSPI;");

            return new CardGameDbContext(optionsBuilder.Options);
        }
    }
}
