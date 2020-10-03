using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame_DataAccess.EntityConfigurations
{
    internal class SubtypeConfiguration : IEntityTypeConfiguration<Subtype>
    {
        #region Public_Methods

        public void Configure(EntityTypeBuilder<Subtype> builder)
        {
            builder.ToTable("CardGame_Subtypes");

            builder.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(1000);
        }

        #endregion // Public_Methods
    }
}
