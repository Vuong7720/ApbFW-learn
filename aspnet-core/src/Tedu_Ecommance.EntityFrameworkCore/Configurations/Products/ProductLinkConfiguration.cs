using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.Products;

namespace Tedu_Ecommance.Configurations.Products
{
    public class ProductLinkConfiguration : IEntityTypeConfiguration<ProductLinks>
    {
        public void Configure(EntityTypeBuilder<ProductLinks> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "ProductLinks");
            builder.HasKey(x => new { x.ProductId, x.LinkedProductId });
        }
    }
}
