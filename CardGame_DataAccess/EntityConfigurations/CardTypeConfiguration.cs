using CardGame_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame_DataAccess.EntityConfigurations
{
    internal class CardTypeConfiguration : IEntityTypeConfiguration<CardType>
    {
        #region Public_Methods

        public void Configure(EntityTypeBuilder<CardType> builder)
        {
            builder.ToTable("CardGame_CardTypes");

            builder.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(1000);
        }

        #endregion // Public_Methods
    }
}
