using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame_DataAccess.EntityConfigurations
{
    internal class CardRuleConfiguration : IEntityTypeConfiguration<CardRule>
    {
        #region Public_Methods

        public void Configure(EntityTypeBuilder<CardRule> builder)
        {
            builder.ToTable("CardGame_CardRules");

            builder.HasKey(st
                => new { st.CardId, st.RuleId });
        }

        #endregion // Public_Methods
    }
}
