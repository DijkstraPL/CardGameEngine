using CardGame_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame_DataAccess.EntityConfigurations
{
     internal class SetConfiguration : IEntityTypeConfiguration<Set>
    {
        #region Public_Methods

        public void Configure(EntityTypeBuilder<Set> builder)
        {
            builder.ToTable("CardGame_Sets");

            builder.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(1000);
        }

        #endregion // Public_Methods
    }
}
