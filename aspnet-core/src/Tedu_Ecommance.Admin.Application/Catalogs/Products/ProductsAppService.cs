using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedu_Ecommance.Admin.Catalogs.Products.Attributes;
using Tedu_Ecommance.ProductAttributes;
using Tedu_Ecommance.ProductCategories;
using Tedu_Ecommance.Products;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Tedu_Ecommance.Admin.Permissions;

namespace Tedu_Ecommance.Admin.Catalogs.Products
{
    [Authorize(Tedu_EcommancePermissions.Product.Default, Policy = "AdminOnly")]
    public class ProductsAppService : CrudAppService<
       Product,
       ProductsDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateProductsDto,
       CreateUpdateProductsDto>, IProductsAppService
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

            GetPolicyName = Tedu_EcommancePermissions.Product.Default;
            GetListPolicyName = Tedu_EcommancePermissions.Product.Default;
            CreatePolicyName = Tedu_EcommancePermissions.Product.Create;
            UpdatePolicyName = Tedu_EcommancePermissions.Product.Update;
            DeletePolicyName = Tedu_EcommancePermissions.Product.Delete;
        }

        [Authorize(Tedu_EcommancePermissions.Product.Create)]
        public override async Task<ProductsDto> CreateAsync(CreateUpdateProductsDto input)
        {
            var products = await _productManager.CreateAsync(
                input.ManufacturerId, input.Name, input.slug,
                input.Description,
                input.SKU, input.Code, input.ProductType,
                input.IsActive,
                input.SeoMetaDescription, input.SellPrice,
                input.SortOder, input.Visibility, input.CategoryId);
            if (input.ThumnailPictureContent != null && input.ThumnailPictureContent.Length > 0)
            {
                await SaveThumnailPictureAsync(input.ThumnailPictureName, input.ThumnailPictureContent);
                products.ThumnailPicture = input.ThumnailPictureName;
            }

            var result = await Repository.InsertAsync(products);
            return ObjectMapper.Map<Product, ProductsDto>(result);
        }

        [Authorize(Tedu_EcommancePermissions.Product.Update)]
        public override async Task<ProductsDto> UpdateAsync(Guid id, CreateUpdateProductsDto input)
        {
            var product = await Repository.GetAsync(id);
            if (product == null)
                throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductIsNotExits);
            product.ManufacturerId = input.ManufacturerId;
            product.Name = input.Name;
            product.slug = input.slug;
            if (input.ThumnailPictureContent != null && input.ThumnailPictureContent.Length > 0)
            {
                await SaveThumnailPictureAsync(input.ThumnailPictureName, input.ThumnailPictureContent);
                product.ThumnailPicture = input.ThumnailPictureName;
            }
            product.Description = input.Description;
            product.SKU = input.SKU;
            product.Code = input.Code;
            product.ProductType = input.ProductType;
            product.SeoMetaDescription = input.SeoMetaDescription;
            product.SellPrice = input.SellPrice;
            product.SortOder = input.SortOder;
            product.Visibility = input.Visibility;
            if (product.CategoryId != input.CategoryId)
            {
                product.CategoryId = input.CategoryId;
                var category = await _productCategoryRepository.GetAsync(x => x.Id == input.CategoryId);
                product.CategoryName = category.Name;
                product.CategorySlug = category.Slug;
            }
            await Repository.UpdateAsync(product);
            return ObjectMapper.Map<Product, ProductsDto>(product);
        }
        [Authorize(Tedu_EcommancePermissions.Product.Delete)]
        public async Task DeleteMutipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        [Authorize(Tedu_EcommancePermissions.Product.Default)]
        public async Task<List<ProductsInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Product>, List<ProductsInListDto>>(data);
        }
        [Authorize(Tedu_EcommancePermissions.Product.Default)]
        public async Task<PagedResultDto<ProductsInListDto>> GetListFilterAsync(ProductListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.keyword), x => x.Name.Contains(input.keyword));
            query = query.WhereIf(input.categoryId.HasValue, x => x.CategoryId == input.categoryId);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.OrderByDescending(x => x.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductsInListDto>(totalCount, ObjectMapper.Map<List<Product>, List<ProductsInListDto>>(data));
        }
        [Authorize(Tedu_EcommancePermissions.Product.Update)]
        private async Task SaveThumnailPictureAsync(string fileName, string base64)
        {
            string base64Data = base64.Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64Data);

            if (await _fileContainer.ExistsAsync(fileName))
            {
                await _fileContainer.DeleteAsync(fileName);
            }

            await _fileContainer.SaveAsync(fileName, bytes);
        }
        [Authorize(Tedu_EcommancePermissions.Product.Default)]
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


        // Add---------------<><><><><>
        [Authorize(Tedu_EcommancePermissions.Product.Update)]
        public async Task<ProductAttributeValueDto> AddAttributeAsync(AddUpdateProductAttributeDto input)
        {
            var product = await Repository.GetAsync(input.ProductId);
            if (product == null)
            {
                throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductIsNotExits);
            }

            var attribute = await _productAttributeRepository.GetAsync(x => x.Id == input.AttributeId);
            if (attribute == null)
            {
                throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
            }

            var newAttributeId = Guid.NewGuid();
            switch (attribute.AttributeType)
            {
                case AttributeType.Date:
                    if (input.DateTimeValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeDateTime = new ProductAttributeDateTime(
                    newAttributeId,
                    input.AttributeId,
                    input.ProductId,
                    input.DateTimeValue
                    );
                    await _productAttributeDateTime.InsertAsync(productAttributeDateTime);
                    break;

                case AttributeType.Decimal:
                    if (input.DecimalValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeDecimal = new ProductAttributeDecimal(
                    newAttributeId,
                    input.AttributeId,
                    input.ProductId,
                    input.DecimalValue
                    );
                    await _productAttributeDecimal.InsertAsync(productAttributeDecimal);
                    break;

                case AttributeType.Int:
                    if (input.IntValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeInt = new ProductAttributeInt(
                    newAttributeId,
                    input.AttributeId,
                    input.ProductId,
                    input.IntValue
                    );
                    await _productAttributeInt.InsertAsync(productAttributeInt);
                    break;

                case AttributeType.Varchar:
                    if (input.VacharValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeVarchar = new ProductAttributeVarchar(
                    newAttributeId,
                    input.AttributeId,
                    input.ProductId,
                    input.VacharValue
                    );
                    await _productAttributeVarchar.InsertAsync(productAttributeVarchar);
                    break;

                case AttributeType.Text:
                    if (input.TextValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeText = new ProductAttributeText(
                    newAttributeId,
                    input.AttributeId,
                    input.ProductId,
                    input.TextValue
                    );
                    await _productAttributeText.InsertAsync(productAttributeText);
                    break;
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new ProductAttributeValueDto()
            {
                AttributeId = input.AttributeId,
                ProductId = input.ProductId,
                DateTimeValue = input.DateTimeValue,
                DecimalValue = input.DecimalValue,
                IntValue = input.IntValue,
                TextValue = input.TextValue,
                Id = newAttributeId,
                Lable = attribute.Lable,

            };
        }


        // Remove---------------<><><><><>
        [Authorize(Tedu_EcommancePermissions.Product.Update)]
        public async Task RemoveAttributeAsync(Guid attributeId, Guid id)
        {

            var attribute = await _productAttributeRepository.GetAsync(x => x.Id == attributeId);
            if (attribute == null)
            {
                throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
            }

            switch (attribute.AttributeType)
            {
                case AttributeType.Date:
                    var productAttributeDateTime = await _productAttributeDateTime.GetAsync(x => x.Id == id);
                    if (productAttributeDateTime == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    await _productAttributeDateTime.DeleteAsync(productAttributeDateTime);
                    break;
                //---
                case AttributeType.Decimal:
                    var productAttributeDecimal = await _productAttributeDecimal.GetAsync(x => x.Id == id);
                    if (productAttributeDecimal == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    await _productAttributeDecimal.DeleteAsync(productAttributeDecimal);
                    break;

                case AttributeType.Int:
                    var productAttributeInt = await _productAttributeInt.GetAsync(x => x.Id == id);
                    if (productAttributeInt == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    await _productAttributeInt.DeleteAsync(productAttributeInt);
                    break;

                case AttributeType.Varchar:
                    var productAttributeVaChar = await _productAttributeVarchar.GetAsync(x => x.Id == id);
                    if (productAttributeVaChar == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    await _productAttributeVarchar.DeleteAsync(productAttributeVaChar);
                    break;

                case AttributeType.Text:
                    var productAttributeText = await _productAttributeText.GetAsync(x => x.Id == id);
                    if (productAttributeText == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    await _productAttributeText.DeleteAsync(productAttributeText);
                    break;
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        // GetListAll---------------<><><><><>
        [Authorize(Tedu_EcommancePermissions.Product.Default)]
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
        [Authorize(Tedu_EcommancePermissions.Product.Default)]
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


        // Update---------------<><><><><>
        [Authorize(Tedu_EcommancePermissions.Product.Update)]
        public async Task<ProductAttributeValueDto> UpdateAttributeAsync(Guid id, AddUpdateProductAttributeDto input)
        {

            var product = await Repository.GetAsync(input.ProductId);
            if (product == null)
            {
                throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductIsNotExits);
            }

            var attribute = await _productAttributeRepository.GetAsync(x => x.Id == input.AttributeId);
            if (attribute == null)
            {
                throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
            }

            switch (attribute.AttributeType)
            {
                case AttributeType.Date:
                    if (input.DateTimeValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeDateTime = await _productAttributeDateTime.GetAsync(x => x.Id == id);
                    if (productAttributeDateTime == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    productAttributeDateTime.Value = input.DateTimeValue.Value;
                    await _productAttributeDateTime.UpdateAsync(productAttributeDateTime);
                    break;
                //---
                case AttributeType.Decimal:
                    if (input.DecimalValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeDecimal = await _productAttributeDecimal.GetAsync(x => x.Id == id);
                    if (productAttributeDecimal == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    productAttributeDecimal.Value = input.DecimalValue.Value;
                    await _productAttributeDecimal.UpdateAsync(productAttributeDecimal);
                    break;

                case AttributeType.Int:
                    if (input.IntValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeInt = await _productAttributeInt.GetAsync(x => x.Id == id);
                    if (productAttributeInt == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    productAttributeInt.Value = input.IntValue.Value;
                    await _productAttributeInt.UpdateAsync(productAttributeInt);
                    break;

                case AttributeType.Varchar:
                    if (input.VacharValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeVaChar = await _productAttributeVarchar.GetAsync(x => x.Id == id);
                    if (productAttributeVaChar == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    productAttributeVaChar.Value = input.VacharValue;
                    await _productAttributeVarchar.UpdateAsync(productAttributeVaChar);
                    break;

                case AttributeType.Text:
                    if (input.TextValue == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeValueIsNotValid);
                    }
                    var productAttributeText = await _productAttributeText.GetAsync(x => x.Id == id);
                    if (productAttributeText == null)
                    {
                        throw new BusinessException(Tedu_EcommanceDomainErrorCodes.ProductAttributeIsNotExits);
                    }
                    productAttributeText.Value = input.TextValue;
                    await _productAttributeText.UpdateAsync(productAttributeText);
                    break;
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new ProductAttributeValueDto()
            {
                AttributeId = input.AttributeId,
                Code = attribute.Code,
                AttributeType = attribute.AttributeType,
                DateTimeValue = input.DateTimeValue,
                DecimalValue = input.DecimalValue,
                Id = id,
                IntValue = input.IntValue,
                Lable = attribute.Lable,
                ProductId = input.ProductId,
                TextValue = input.TextValue

            };
        }


    }
}
