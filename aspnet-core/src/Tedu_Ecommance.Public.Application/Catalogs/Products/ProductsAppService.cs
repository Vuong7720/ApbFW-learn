using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedu_Ecommance.Public.Catalogs.Products.Attributes;
using Tedu_Ecommance.ProductAttributes;
using Tedu_Ecommance.ProductCategories;
using Tedu_Ecommance.Products;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;

namespace Tedu_Ecommance.Public.Catalogs.Products
{
    public class ProductsAppService : CrudAppService<
       Product,
       ProductsDto,
       Guid,
       PagedResultRequestDto>, IProductsAppService
    {
        private readonly ProductManager _productManager;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IBlobContainer<ProductThumnailPictureContainer> _fileContainer;
        private readonly ProductCodeGenarator _productCodeGenarator;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<ProductAttributeDateTime> _productAttributeDateTime;
        private readonly IRepository<ProductAttributeDecimal> _productAttributeDecimal;
        private readonly IRepository<ProductAttributeInt> _productAttributeInt;
        private readonly IRepository<ProductAttributeText> _productAttributeText;
        private readonly IRepository<ProductAttributeVarchar> _productAttributeVarchar;
        public ProductsAppService(IRepository<Product, Guid> repository,
            IRepository<ProductCategory> productCategoryRepository,
            IBlobContainer<ProductThumnailPictureContainer> fileContainer,
            ProductManager productManager,
            ProductCodeGenarator productCodeGenarator,
             IRepository<ProductAttribute> productAttributeRepository,
             IRepository<ProductAttributeDateTime> productAttributeDateTime,
             IRepository<ProductAttributeDecimal> productAttributeDecimal,
             IRepository<ProductAttributeInt> productAttributeInt,
            IRepository<ProductAttributeText> productAttributeText,
            IRepository<ProductAttributeVarchar> productAttributeVarchar

             ) : base(repository)
        {
            _productManager = productManager;
            _productCategoryRepository = productCategoryRepository;
            _fileContainer = fileContainer;
            _productCodeGenarator = productCodeGenarator;
            _productAttributeRepository = productAttributeRepository;
            _productAttributeDateTime = productAttributeDateTime;
            _productAttributeDecimal = productAttributeDecimal;
            _productAttributeInt = productAttributeInt;
            _productAttributeText = productAttributeText;
            _productAttributeVarchar = productAttributeVarchar;
        }

   
        public async Task<List<ProductsInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Product>, List<ProductsInListDto>>(data);
        }
        public async Task<PagedResultDto<ProductsInListDto>> GetListFilterAsync(ProductListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.keyword), x => x.Name.Contains(input.keyword));
            query = query.WhereIf(input.categoryId.HasValue, x => x.CategoryId == input.categoryId);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.OrderByDescending(x => x.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductsInListDto>(totalCount, ObjectMapper.Map<List<Product>, List<ProductsInListDto>>(data));
        }
        public async Task<string> GetThumnailImageAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var thumnailContent = await _fileContainer.GetAllBytesOrNullAsync(fileName);
            if (thumnailContent is null)
            {
                return null;
            }
            var result = Convert.ToBase64String(thumnailContent);
            return result;
        }

        public async Task<string> GetsuggestNumber()
        {
            return await _productCodeGenarator.GenerateAsync();
        }

        public async Task<List<ProductAttributeValueDto>> GetListProductAttributeAllAsync(Guid productId)
        {
            var attributeQuery = await _productAttributeRepository.GetQueryableAsync();
            var attributeDatetimeQuery = await _productAttributeDateTime.GetQueryableAsync();
            var attributeDecimalQuery = await _productAttributeDecimal.GetQueryableAsync();
            var attributeIntQuery = await _productAttributeInt.GetQueryableAsync();
            var attributeTextQuery = await _productAttributeText.GetQueryableAsync();
            var attributeVarcharQuery = await _productAttributeVarchar.GetQueryableAsync();

            var query = from a in attributeQuery
                        join adate in attributeDatetimeQuery on a.Id equals adate.AttributeId into aDateTimeTable
                        from adate in aDateTimeTable.DefaultIfEmpty()
                        join adecimal in attributeDecimalQuery on a.Id equals adecimal.AttributeId into aDecimalTable
                        from adecimal in aDecimalTable.DefaultIfEmpty()
                        join aint in attributeIntQuery on a.Id equals aint.AttributeId into aIntTable
                        from aint in aIntTable.DefaultIfEmpty()
                        join atext in attributeTextQuery on a.Id equals atext.AttributeId into aTextTable
                        from atext in aTextTable.DefaultIfEmpty()
                        join avarchar in attributeVarcharQuery on a.Id equals avarchar.AttributeId into aVarcharTable
                        from avarchar in aVarcharTable.DefaultIfEmpty()
                        where
                        (adate == null || adate.ProductId == productId) &&
                        (adecimal == null || adecimal.ProductId == productId) &&
                        (aint == null || aint.ProductId == productId) &&
                        (atext == null || atext.ProductId == productId) &&
                        (avarchar == null || avarchar.ProductId == productId)
                        select new ProductAttributeValueDto()
                        {
                            Lable = a.Lable,
                            AttributeId = a.Id,
                            Code = a.Code,
                            AttributeType = a.AttributeType,
                            ProductId = productId,
                            DateTimeValue = adate != null ? adate.Value : null,
                            DecimalValue = adecimal != null ? adecimal.Value : null,
                            IntValue = aint != null ? aint.Value : null,
                            TextValue = atext != null ? atext.Value : null,
                            VarcharValue = avarchar != null ? avarchar.Value : null,

                            DateTimeId = adate != null ? adate.Id : null,
                            DecimalId = adecimal != null ? adecimal.Id : null,
                            IntId = aint != null ? aint.Id : null,
                            TextId = atext != null ? atext.Id : null,
                            VarcharId = avarchar != null ? avarchar.Id : null,
                        };
            query = query.Where(x => x.DateTimeId != null
           || x.DecimalId != null
           || x.IntId != null
           || x.TextId != null
           || x.VarcharId != null);
            return await AsyncExecuter.ToListAsync(query);
        }
        // GetItems---------------<><><><><>
        public async Task<PagedResultDto<ProductAttributeValueDto>> GetProductAttributeAsync(ProductAttributeListFilterDto input)
        {
            var attributeQuery = await _productAttributeRepository.GetQueryableAsync();

            var attributeDatetimeQuery = await _productAttributeDateTime.GetQueryableAsync();
            var attributeDecimalQuery = await _productAttributeDecimal.GetQueryableAsync();
            var attributeIntQuery = await _productAttributeInt.GetQueryableAsync();
            var attributeTextQuery = await _productAttributeText.GetQueryableAsync();
            var attributeVarcharQuery = await _productAttributeVarchar.GetQueryableAsync();

            var query = from a in attributeQuery
                        join adate in attributeDatetimeQuery on a.Id equals adate.AttributeId into aDateTimeTable
                        from adate in aDateTimeTable.DefaultIfEmpty()
                        join adecimal in attributeDecimalQuery on a.Id equals adecimal.AttributeId into aDecimalTable
                        from adecimal in aDecimalTable.DefaultIfEmpty()
                        join aint in attributeIntQuery on a.Id equals aint.AttributeId into aIntTable
                        from aint in aIntTable.DefaultIfEmpty()
                        join atext in attributeTextQuery on a.Id equals atext.AttributeId into aTextTable
                        from atext in aTextTable.DefaultIfEmpty()
                        join avarchar in attributeVarcharQuery on a.Id equals avarchar.AttributeId into aVarcharTable
                        from avarchar in aVarcharTable.DefaultIfEmpty()
                        where
                        (adate == null || adate.ProductId == input.ProductId) &&
                        (adecimal == null || adecimal.ProductId == input.ProductId) &&
                        (aint == null || aint.ProductId == input.ProductId) &&
                        (atext == null || atext.ProductId == input.ProductId) &&
                        (avarchar == null || avarchar.ProductId == input.ProductId)
                        select new ProductAttributeValueDto()
                        {
                            Lable = a.Lable,
                            AttributeId = a.Id,
                            Code = a.Code,
                            AttributeType = a.AttributeType,
                            ProductId = input.ProductId,
                            DateTimeValue = adate != null ? adate.Value : null,
                            DecimalValue = adecimal != null ? adecimal.Value : null,
                            IntValue = aint != null ? aint.Value : null,
                            TextValue = atext != null ? atext.Value : null,
                            VarcharValue = avarchar != null ? avarchar.Value : null,

                            DateTimeId = adate != null ? adate.Id : null,
                            DecimalId = adecimal != null ? adecimal.Id : null,
                            IntId = aint != null ? aint.Id : null,
                            TextId = atext != null ? atext.Id : null,
                            VarcharId = avarchar != null ? avarchar.Id : null,
                        };
            query = query.Where(x => x.DateTimeId != null
            || x.DecimalId != null
            || x.IntId != null
            || x.TextId != null
            || x.VarcharId != null);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync
                (query.OrderByDescending(x => x.Lable)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount));

            return new PagedResultDto<ProductAttributeValueDto>(totalCount, data);

        }


    }
}
