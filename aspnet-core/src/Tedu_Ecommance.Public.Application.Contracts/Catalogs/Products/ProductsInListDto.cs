using System;
using System.Collections.Generic;
using System.Text;
using Tedu_Ecommance.Products;
using Volo.Abp.Application.Dtos;

namespace Tedu_Ecommance.Public.Catalogs.Products
{
    public class ProductsInListDto : EntityDto<Guid>
    {
        public Guid ManufacturerId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public ProductType ProductType { get; set; }
        public string? SKU { get; set; }
        public int SortOder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public string? ThumnailPicture { get; set; }
        public string? CategoryName { get; set; }
        public string? CategorySlug { get; set; }

    }
}
