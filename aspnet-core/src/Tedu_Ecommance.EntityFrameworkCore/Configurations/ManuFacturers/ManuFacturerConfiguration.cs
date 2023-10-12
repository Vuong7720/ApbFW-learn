using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.Inventories;
using System.Diagnostics.Metrics;

namespace Tedu_Ecommance.ManuFacturers
{
    public class ManuFacturerConfiguration : IEntityTypeConfiguration<ManuFacturer>
    {
        public void Configure(EntityTypeBuilder<ManuFacturer> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "ManuFacturers");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(x => x.Slug)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(x => x.CoverPicture)
                .HasMaxLength(250)
                .IsRequired();

        }
    }
}
