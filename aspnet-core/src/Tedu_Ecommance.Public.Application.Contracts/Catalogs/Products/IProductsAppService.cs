using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tedu_Ecommance.Public.Catalogs.Products.Attributes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Public.Catalogs.Products
{
    public interface IProductsAppService : IReadOnlyAppService
       <ProductsDto,
       Guid,
       PagedResultRequestDto>
    {
        Task<PagedResultDto<ProductsInListDto>> GetListFilterAsync(ProductListFilterDto input);
        Task<List<ProductsInListDto>> GetListAllAsync();
        Task<string> GetThumnailImageAsync(string fileName);
        Task<string> GetsuggestNumber();
        Task<List<ProductAttributeValueDto>> GetListProductAttributeAllAsync(Guid productId);
        Task<PagedResultDto<ProductAttributeValueDto>> GetProductAttributeAsync(ProductAttributeListFilterDto input);
    }
}
