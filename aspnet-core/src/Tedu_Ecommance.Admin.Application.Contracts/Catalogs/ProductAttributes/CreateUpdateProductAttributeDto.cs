using System;
using System.Collections.Generic;
using System.Text;
using Tedu_Ecommance.ProductAttributes;

namespace Tedu_Ecommance.Admin.Catalogs.ProductAttributes
{
    public class CreateUpdateProductAttributeDto
    {
        public string? Code { get; set; }
        public AttributeType AttributeType { get; set; }
        public string? Lable { get; set; }
        public string? SortOder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
        public bool IsUnique { get; set; }
        public string? Note { get; set; }
    }
}