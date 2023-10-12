using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tedu_Ecommance.InventoryTickets
{
    public class InventoryTicketItems : Entity<Guid>
    {
        public Guid InventoryTicketId { get; set; }
        public Guid ProductId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpriedDate { get; set; }
    }
}
