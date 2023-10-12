using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.Inventories;
using Tedu_Ecommance.Promotions;

namespace Tedu_Ecommance.Promotions
{
    public class PromotionManuFacturerConfiguration : IEntityTypeConfiguration<PromotionManufactures>
    {
        public void Configure(EntityTypeBuilder<PromotionManufactures> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "PromotionManufactures");
            builder.HasKey(x => x.Id);
           
        }
    }
}
