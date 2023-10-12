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
    public class ProductAttributeIntConfiguration : IEntityTypeConfiguration<ProductAttributeInt>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeInt> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "ProductAttributeInts");
            builder.HasKey(x => x.Id);
            
        }
    }
}
