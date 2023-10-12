using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.Inventories;
using Tedu_Ecommance.Orders;

namespace Tedu_Ecommance.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "Orders");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.CustomerName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.CustomerPhoneNumber)
                .HasMaxLength(15)
                .IsRequired();
            builder.Property(x => x.CustomerAddress)
                .HasMaxLength(250)
                .IsRequired();

        }
    }
}
