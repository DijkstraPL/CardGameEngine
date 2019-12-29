using CardGame_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame_DataAccess.EntityConfigurations
{
    internal class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        #region Public_Methods

        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("CardGame_Cards");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(c => c.Description)
                .IsRequired();

            builder.HasMany<CardRule>(c => c.Rules)
                .WithOne(cr => cr.Card)
                .HasForeignKey(cr => cr.CardId);
        }

        #endregion // Public_Methods
    }
}
