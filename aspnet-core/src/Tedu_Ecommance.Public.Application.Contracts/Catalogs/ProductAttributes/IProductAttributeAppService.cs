using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Public.Catalogs.ProductAttributes
{
    public interface IProductAttributeAppService : IReadOnlyAppService
       <ProductAttributeDto,
       Guid,
       PagedResultRequestDto>
    {
        Task<PagedResultDto<ProductAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ProductAttributeInListDto>> GetListAllAsync();
    }
}
