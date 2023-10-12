﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tedu_Ecommance.Products
{
    public class ProductTags : Entity
    {
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; }

        public override object?[] GetKeys()
        {
            return new object?[] { ProductId, TagId };  
        }
    }
}