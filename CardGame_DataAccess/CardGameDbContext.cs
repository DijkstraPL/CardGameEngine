﻿using CardGame_DataAccess.Entities;
using CardGame_DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CardGame_DataAccess
{
    public class CardGameDbContext : DbContext
    {
        #region Properties

        public DbSet<Card> Cards { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Subtype> Subtypes { get; set; }
        public DbSet<Deck> Decks { get; set; }

        #endregion // Properties

        #region Fields

        static private IConfigurationRoot _configuration;

        #endregion // Fields  
        
        #region Constructors

        public CardGameDbContext(DbContextOptions<CardGameDbContext> options)
            : base(options)
        {
        }

        #endregion // Constructors

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new RuleConfiguration());
            modelBuilder.ApplyConfiguration(new CardRuleConfiguration());
            modelBuilder.ApplyConfiguration(new CardTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SetConfiguration());
            modelBuilder.ApplyConfiguration(new SubtypeConfiguration());
            modelBuilder.ApplyConfiguration(new DeckConfiguration());
            modelBuilder.ApplyConfiguration(new CardDeckConfiguration());
        }
    }
}
