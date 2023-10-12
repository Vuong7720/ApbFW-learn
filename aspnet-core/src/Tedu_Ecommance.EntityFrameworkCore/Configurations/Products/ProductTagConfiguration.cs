using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.Products;

namespace Tedu_Ecommance.Products
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTags>
    {
        public void Configure(EntityTypeBuilder<ProductTags> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "ProductTags");
            builder.HasKey(x => new { x.ProductId, x.TagId});
        }
    }
}
