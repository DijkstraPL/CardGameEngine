using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame_DataAccess.EntityConfigurations
{
    internal class CardDeckConfiguration : IEntityTypeConfiguration<CardDeck>
    {
        #region Public_Methods

        public void Configure(EntityTypeBuilder<CardDeck> builder)
        {
            builder.ToTable("CardGame_CardDecks");

            builder.HasKey(st
                => new { st.CardId, st.DeckId });
        }

        #endregion // Public_Methods
    }
}
