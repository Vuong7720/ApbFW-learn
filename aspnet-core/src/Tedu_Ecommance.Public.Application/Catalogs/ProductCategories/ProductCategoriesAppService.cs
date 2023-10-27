﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedu_Ecommance.ProductCategories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;



namespace Tedu_Ecommance.Public.Catalogs.ProductCategories
{
    public class ProductsCategoriesAppService : ReadOnlyAppService<
       ProductCategory,
       ProductCategoryDto,
       Guid,
       PagedResultRequestDto
       >, IProductCategoriesAppService
    {

        public ProductsCategoriesAppService(IRepository<ProductCategory, Guid> repository)
            : base(repository)
        {
        }

        public async Task<List<ProductCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data);
        }


        public async Task<PagedResultDto<ProductCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.keyword), x => x.Name.Contains(input.keyword ?? ""));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductCategoryInListDto>(totalCount, ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data));
        }

    }
}
