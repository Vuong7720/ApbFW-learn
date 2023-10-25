using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Admin.Catalogs.ProductCategories
{
    public interface IProductCategoriesAppService : ICrudAppService
       <ProductCategoryDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateProductCategoryDto,
       CreateUpdateProductCategoryDto>
    {
        Task<PagedResultDto<ProductCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ProductCategoryInListDto>> GetListAllAsync();
        Task DeleteMutipleAsync(IEnumerable<Guid> ids);
    }
}
