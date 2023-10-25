using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.Inventories;

namespace Tedu_Ecommance.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "Products");
            builder.HasKey(x => x.Id);

            builder.Property(x =>x.Name)
                .HasMaxLength(50);
            builder.Property(x =>x.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(x =>x.SKU)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(x =>x.SeoMetaDescription)
                .HasMaxLength(250);
            builder.Property(x => x.Description);

            builder.Property(x => x.ThumnailPicture)
                .HasMaxLength(250);

            builder.Property(x => x.CategoryName)
                .HasMaxLength(250);

            builder.Property(x => x.CategorySlug)
                .HasMaxLength(250);



        }
    }
}
