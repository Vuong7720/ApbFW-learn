using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Admin.Catalogs.Manufacturers
{
    public interface IManufacturerAppService : ICrudAppService
       <ManufacturerDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateManufacturerDto,
       CreateUpdateManufacturerDto>
    {
        Task<PagedResultDto<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ManufacturerInListDto>> GetListAllAsync();
        Task DeleteMutipleAsync(IEnumerable<Guid> ids);
    }
}
