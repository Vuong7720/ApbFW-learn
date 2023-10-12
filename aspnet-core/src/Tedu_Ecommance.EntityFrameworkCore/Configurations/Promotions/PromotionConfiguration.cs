using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.Inventories;
using static System.Formats.Asn1.AsnWriter;

namespace Tedu_Ecommance.Promotions
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "Promotions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50);
            builder.Property(x => x.CouponCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(x => x.Scope)
                .HasMaxLength(250);



        }
    }
}
