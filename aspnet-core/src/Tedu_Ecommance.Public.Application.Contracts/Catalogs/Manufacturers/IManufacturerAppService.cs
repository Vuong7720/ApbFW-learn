using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Public.Catalogs.Manufacturers
{
    public interface IManufacturerAppService : IReadOnlyAppService
       <ManufacturerDto,
       Guid,
       PagedResultRequestDto>
    {
        Task<PagedResultDto<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ManufacturerInListDto>> GetListAllAsync();
    }
}
