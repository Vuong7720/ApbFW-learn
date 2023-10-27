using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedu_Ecommance.Admin;
using Tedu_Ecommance.ManuFacturers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Tedu_Ecommance.Admin.Permissions;

namespace Tedu_Ecommance.Admin.Catalogs.Manufacturers
{
    [Authorize(Tedu_EcommancePermissions.Manufacture.Default, Policy="AdminOnly")]
    public class ManufacturersAppService : CrudAppService<
        ManuFacturer,
        ManufacturerDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateManufacturerDto,
        CreateUpdateManufacturerDto>, IManufacturerAppService
    {
        public ManufacturersAppService(IRepository<ManuFacturer, Guid> repository)
            : base(repository)
        {
            GetPolicyName = Tedu_EcommancePermissions.Manufacture.Default;
            GetListPolicyName = Tedu_EcommancePermissions.Manufacture.Default;
            CreatePolicyName = Tedu_EcommancePermissions.Manufacture.Create;
            UpdatePolicyName = Tedu_EcommancePermissions.Manufacture.Update;
            DeletePolicyName = Tedu_EcommancePermissions.Manufacture.Delete;
        }
        [Authorize(Tedu_EcommancePermissions.Manufacture.Delete)]
        public async Task DeleteMutipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(Tedu_EcommancePermissions.Manufacture.Default)]
        public async Task<List<ManufacturerInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ManuFacturer>, List<ManufacturerInListDto>>(data);

        }

        [Authorize(Tedu_EcommancePermissions.Manufacture.Default)]
        public async Task<PagedResultDto<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.keyword), x => x.Name.Contains(input.keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ManufacturerInListDto>(totalCount, ObjectMapper.Map<List<ManuFacturer>, List<ManufacturerInListDto>>(data));
        }
    }
}