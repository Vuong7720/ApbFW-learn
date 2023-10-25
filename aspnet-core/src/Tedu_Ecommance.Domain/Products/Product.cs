using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tedu_Ecommance.Products
{
    public class Product : AuditedAggregateRoot<Guid>
    {
        public Product() { }
        public Product(Guid id,Guid manufacturerId, string? name, 
            double sellPrice, string? slug, string? code, 
            ProductType productType, string? sKU, int sortOder, 
            bool visibility, bool isActive, Guid categoryId, 
            string? seoMetaDescription, string? description, 
            string? thumnailPicture, string? categoryName, 
            string? categorySlug)
        {
            Id = id;
            ManufacturerId = manufacturerId;
            Name = name;
            SellPrice = sellPrice;
            this.slug = slug;
            Code = code;
            ProductType = productType;
            SKU = sKU;
            SortOder = sortOder;
            Visibility = visibility;
            IsActive = isActive;
            CategoryId = categoryId;
            SeoMetaDescription = seoMetaDescription;
            Description = description;
            ThumnailPicture = thumnailPicture;
            CategoryName = categoryName;
            CategorySlug = categorySlug;
        }

        public Guid ManufacturerId { get; set; }
        public string? Name { get; set; }
        public double SellPrice { get; set; }
        public string? slug { get; set; }
        public string? Code { get; set; }
        public ProductType ProductType { get; set; }
        public string? SKU { get; set; }
        public int SortOder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public string? SeoMetaDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumnailPicture {  get; set; }
        public string? CategoryName { get; set; }
        public string? CategorySlug { get; set; }
    }
}
