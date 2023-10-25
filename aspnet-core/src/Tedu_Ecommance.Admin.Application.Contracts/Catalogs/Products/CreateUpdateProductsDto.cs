using System;
using System.Collections.Generic;
using System.Text;
using Tedu_Ecommance.Products;

namespace Tedu_Ecommance.Admin.Catalogs.Products
{
    public class CreateUpdateProductsDto
    {
        public Guid ManufacturerId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? slug { get; set; }
        public ProductType ProductType { get; set; }
        public string? SKU { get; set; }
        public int SortOder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public double SellPrice { get; set; }
        public Guid CategoryId { get; set; }
        public string? SeoMetaDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumnailPictureName { get; set; }
        public string? ThumnailPictureContent { get; set; }
    }
}