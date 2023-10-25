using System;
using System.Collections.Generic;
using System.Text;

namespace Tedu_Ecommance.Admin.Catalogs.Products
{
    public class ProductListFilterDto : BaseListFilterDto
    {
        public Guid? categoryId { get; set; }
    }
}
