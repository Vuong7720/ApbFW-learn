﻿using System;
using System.Collections.Generic;
using System.Text;
using Tedu_Ecommance.ProductAttributes;
using Volo.Abp.Application.Dtos;

namespace Tedu_Ecommance.Admin.Catalogs.Products.Attributes
{
    public class ProductAttributeValueDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid AttributeId { get; set; }
        public AttributeType AttributeType { get; set; }
        public string? Code { get; set; }
        public string? Lable { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public int? IntValue { get; set; }
        public string? TextValue { get; set; }
        public string? VarcharValue { get; set; }


        public Guid? DateTimeId { get; set; }
        public Guid? DecimalId { get; set; }
        public Guid? IntId { get; set; }
        public Guid? TextId { get; set; }
        public Guid? VarcharId { get; set; }
    }
}
