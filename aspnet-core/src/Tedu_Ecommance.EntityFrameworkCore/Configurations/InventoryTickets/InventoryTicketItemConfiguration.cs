using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.InventoryTickets;

namespace Tedu_Ecommance.InventoryTickets
{
    public class InventoryTicketItemConfiguration : IEntityTypeConfiguration<InventoryTicketItems>
    {
        public void Configure(EntityTypeBuilder<InventoryTicketItems> builder)
        {
            builder.ToTable(Tedu_EcommanceConsts.DbTablePrefix + "InventoryTicketItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SKU)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(x => x.BatchNumber)
                .HasMaxLength(50)
                .IsUnicode (false);
        }
    }
}
