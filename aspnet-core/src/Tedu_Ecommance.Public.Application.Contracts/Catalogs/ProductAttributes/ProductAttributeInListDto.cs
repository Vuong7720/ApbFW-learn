using System;
using System.Collections.Generic;
using System.Text;
using Tedu_Ecommance.ProductAttributes;
using Volo.Abp.Application.Dtos;

namespace Tedu_Ecommance.Public.Catalogs.ProductAttributes
{
    public class ProductAttributeInListDto : EntityDto<Guid>
    {
        public string? Code { get; set; }
        public AttributeType AttributeType { get; set; }
        public string? Lable { get; set; }
        public string? SortOder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
        public bool IsUnique { get; set; }
        public Guid Id { get; set; }

    }
}
