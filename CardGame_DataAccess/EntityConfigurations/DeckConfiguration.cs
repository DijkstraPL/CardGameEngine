using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_DataAccess.EntityConfigurations
{
   internal  class DeckConfiguration : IEntityTypeConfiguration<Deck>
    {
        #region Public_Methods

        public void Configure(EntityTypeBuilder<Deck> builder)
        {
            builder.ToTable("CardGame_Decks");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .HasMaxLength(70);

            builder.HasMany<CardDeck>(d => d.Cards)
                .WithOne(cd => cd.Deck)
                .HasForeignKey(cd => cd.DeckId);
        }

        #endregion // Public_Methods
    }
}
