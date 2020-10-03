using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame_DataAccess.EntityConfigurations
{
    internal class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        #region Public_Methods

        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.ToTable("CardGame_Rules");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Condition)
                .HasMaxLength(1000);

            builder.Property(r => r.Effect)
                .HasMaxLength(1000);

            builder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(1000);
        }

        #endregion // Public_Methods
    }
}
