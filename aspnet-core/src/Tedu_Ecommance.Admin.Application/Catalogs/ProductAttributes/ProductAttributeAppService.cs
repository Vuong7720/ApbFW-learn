using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedu_Ecommance.ProductAttributes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Tedu_Ecommance.Admin.Permissions;

namespace Tedu_Ecommance.Admin.Catalogs.ProductAttributes
{
    [Authorize(Tedu_EcommancePermissions.Attribute.Default, Policy = "AdminOnly")]
    public class ProductAttributeAppService : CrudAppService<
       ProductAttribute,
       ProductAttributeDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateProductAttributeDto,
       CreateUpdateProductAttributeDto>, IProductAttributeAppService
    {

        public ProductAttributeAppService(IRepository<ProductAttribute, Guid> repository)
            : base(repository)
        {
            GetPolicyName = Tedu_EcommancePermissions.Attribute.Default;
            GetListPolicyName = Tedu_EcommancePermissions.Attribute.Default;
            CreatePolicyName = Tedu_EcommancePermissions.Attribute.Create;
            UpdatePolicyName = Tedu_EcommancePermissions.Attribute.Update;
            DeletePolicyName = Tedu_EcommancePermissions.Attribute.Delete;
        }
        [Authorize(Tedu_EcommancePermissions.Attribute.Delete)]
        public async Task DeleteMutipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        [Authorize(Tedu_EcommancePermissions.Attribute.Default)]
        public async Task<List<ProductAttributeInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeInListDto>>(data);
        }
        [Authorize(Tedu_EcommancePermissions.Attribute.Default)]
        public async Task<PagedResultDto<ProductAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.keyword), x => x.Lable.Contains(input.keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductAttributeInListDto>(totalCount, ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeInListDto>>(data));
        }

    }
}
