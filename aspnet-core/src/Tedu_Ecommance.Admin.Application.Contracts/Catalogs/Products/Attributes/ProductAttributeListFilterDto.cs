using System;
using System.Collections.Generic;
using System.Text;

namespace Tedu_Ecommance.Admin.Catalogs.Products.Attributes
{
    public class ProductAttributeListFilterDto : BaseListFilterDto
    {
        public Guid ProductId { get; set; }
    }
}
