using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tedu_Ecommance.ProductAttributes
{
    public class ProductAttribute : CreationAuditedAggregateRoot<Guid>
    {
        public string Code { get; set; }
        public AttributeType AttributeType { get; set; }
        public string Lable { get; set; }
        public string SortOder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
        public bool IsUnique { get; set; }
        public string Note { get; set; }
    }
}
