using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tedu_Ecommance.Admin.Catalogs.Products.Attributes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Admin.Catalogs.Products
{
    public interface IProductsAppService : ICrudAppService
       <ProductsDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateProductsDto,
       CreateUpdateProductsDto>
    {
        Task<PagedResultDto<ProductsInListDto>> GetListFilterAsync(ProductListFilterDto input);
        Task<List<ProductsInListDto>> GetListAllAsync();
        Task DeleteMutipleAsync(IEnumerable<Guid> ids);
        Task<string> GetThumnailImageAsync(string fileName);
        Task<string> GetsuggestNumber();
        Task<ProductAttributeValueDto> AddAttributeAsync(AddUpdateProductAttributeDto input);
        Task<ProductAttributeValueDto> UpdateAttributeAsync(Guid id, AddUpdateProductAttributeDto input);
        Task RemoveAttributeAsync(Guid attributeId, Guid id);
        Task<List<ProductAttributeValueDto>> GetListProductAttributeAllAsync(Guid productId);
        Task<PagedResultDto<ProductAttributeValueDto>> GetProductAttributeAsync(ProductAttributeListFilterDto input);
    }
}
