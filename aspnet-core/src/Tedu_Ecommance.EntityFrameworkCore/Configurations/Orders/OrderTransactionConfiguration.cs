using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.Inventories;
using Tedu_Ecommance.Orders;

namespace Tedu_Ecommance.Configurations.Orders
{
    internal class OrderTransactionConfiguration : IEntityTypeConfiguration<OrderTransactions>
    {
        public void Configure(EntityTypeBuilder<OrderTransactions> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "OrderTransactions");
            builder.Property(x => x.Code)
                 .HasMaxLength(50)
                 .IsUnicode(false)
                 .IsRequired();
        }
    }
}
