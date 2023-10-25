using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.ProductCategories;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Tedu_Ecommance.Products
{
    public class ProductManager : DomainService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;

        public ProductManager(IRepository<Product, Guid> productRepository,
             IRepository<ProductCategory, Guid> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync
            (Guid manufacturerId, string? name,
            string? slug, string? description, string? sKU,
            string? code, ProductType productType, bool isActive, string? seoMetaDescription,
            double sellPrice, int sortOder, bool visibility, Guid categoryId)
        {
            if (await _productRepository.AnyAsync(x => x.Name == name))
                throw new UserFriendlyException("ten san pham da ton tai", Tedu_EcommanceDomainErrorCodes.ProductNameAlreadyExits);

            if (await _productRepository.AnyAsync(x => x.Code == code))
                throw new UserFriendlyException("code san pham da ton tai", Tedu_EcommanceDomainErrorCodes.ProductCodeAlreadyExits);
            if (await _productRepository.AnyAsync(x => x.SKU == sKU))
                throw new UserFriendlyException("SKU san pham da ton tai", Tedu_EcommanceDomainErrorCodes.ProductSkuAlreadyExits);

            var category = await _productCategoryRepository.GetAsync(categoryId);

            return new Product(Guid.NewGuid(), manufacturerId,
                name, sellPrice, slug, code, productType, sKU,
                sortOder, visibility, isActive, categoryId,
                seoMetaDescription, description, null,
                category?.Name, category?.Slug);
        }
    }
       
}
