using System;
using System.Collections.Generic;
using System.Text;
using Tedu_Ecommance.ProductAttributes;
using Volo.Abp.Application.Dtos;

namespace Tedu_Ecommance.Admin.Catalogs.ProductAttributes
{
    public class ProductAttributeDto : IEntityDto<Guid>
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
        public Guid Id { get; set; }
    }
}