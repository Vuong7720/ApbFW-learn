using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedu_Ecommance.ProductCategories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Tedu_Ecommance.Admin.Permissions;



namespace Tedu_Ecommance.Admin.Catalogs.ProductCategories
{
    [Authorize(Tedu_EcommancePermissions.Category.Default, Policy = "AdminOnly")]
    public class ProductsCategoriesAppService : CrudAppService<
       ProductCategory,
       ProductCategoryDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateProductCategoryDto,
       CreateUpdateProductCategoryDto>, IProductCategoriesAppService
    {

        public ProductsCategoriesAppService(IRepository<ProductCategory, Guid> repository)
            : base(repository)
        {
            GetPolicyName = Tedu_EcommancePermissions.Category.Default;
            GetListPolicyName = Tedu_EcommancePermissions.Category.Default;
            CreatePolicyName = Tedu_EcommancePermissions.Category.Create;
            UpdatePolicyName = Tedu_EcommancePermissions.Category.Update;
            DeletePolicyName = Tedu_EcommancePermissions.Category.Delete;
        }
        [Authorize(Tedu_EcommancePermissions.Category.Delete)]
        public async Task DeleteMutipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        [Authorize(Tedu_EcommancePermissions.Category.Default)]
        public async Task<List<ProductCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data);
        }
        [Authorize(Tedu_EcommancePermissions.Category.Default)]
        public async Task<PagedResultDto<ProductCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.keyword), x => x.Name.Contains(input.keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductCategoryInListDto>(totalCount, ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data));
        }

    }
}
