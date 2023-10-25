using System;
using System.Collections.Generic;
using System.Text;

namespace Tedu_Ecommance.Admin.Catalogs.Products.Attributes
{
    public class AddUpdateProductAttributeDto
    {
        public Guid ProductId { get; set; }
        public Guid AttributeId { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public int? IntValue { get; set; }
        public string? TextValue { get; set; }
        public string? VacharValue { get; set; }
    }
}
