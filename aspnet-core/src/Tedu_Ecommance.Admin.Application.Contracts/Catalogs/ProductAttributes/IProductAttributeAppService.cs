using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Admin.Catalogs.ProductAttributes
{
    public interface IProductAttributeAppService : ICrudAppService
       <ProductAttributeDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateProductAttributeDto,
       CreateUpdateProductAttributeDto>
    {
        Task<PagedResultDto<ProductAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ProductAttributeInListDto>> GetListAllAsync();
        Task DeleteMutipleAsync(IEnumerable<Guid> ids);
    }
}
