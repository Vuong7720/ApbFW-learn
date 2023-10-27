using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedu_Ecommance.Public;
using Tedu_Ecommance.ManuFacturers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Tedu_Ecommance.Public.Permissions;

namespace Tedu_Ecommance.Public.Catalogs.Manufacturers
{
    public class ManufacturersAppService :ReadOnlyAppService<
        ManuFacturer,
        ManufacturerDto,
        Guid,
        PagedResultRequestDto>, IManufacturerAppService
    {
        public ManufacturersAppService(IRepository<ManuFacturer, Guid> repository)
            : base(repository)
        {
      
        }
        public async Task<List<ManufacturerInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ManuFacturer>, List<ManufacturerInListDto>>(data);

        }
        public async Task<PagedResultDto<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.keyword), x => x.Name.Contains(input.keyword ?? ""));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ManufacturerInListDto>(totalCount, ObjectMapper.Map<List<ManuFacturer>, List<ManufacturerInListDto>>(data));
        }
    }
}