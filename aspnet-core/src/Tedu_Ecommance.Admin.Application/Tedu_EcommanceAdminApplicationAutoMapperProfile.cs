using AutoMapper;
using Tedu_Ecommance.Admin.Catalogs.Manufacturers;
using Tedu_Ecommance.Admin.Catalogs.ProductAttributes;
using Tedu_Ecommance.Admin.Catalogs.ProductCategories;
using Tedu_Ecommance.Admin.Catalogs.Products;
using Tedu_Ecommance.Admin.Systems.Roles;
using Tedu_Ecommance.Admin.Systems.Users;
using Tedu_Ecommance.ManuFacturers;
using Tedu_Ecommance.ProductAttributes;
using Tedu_Ecommance.ProductCategories;
using Tedu_Ecommance.Products;
using Tedu_Ecommance.Roles;
using Volo.Abp.Identity;

namespace Tedu_Ecommance.Admin;

public class Tedu_EcommanceAdminApplicationAutoMapperProfile : Profile
{
    public Tedu_EcommanceAdminApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // category
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();
        CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();


        // product
        CreateMap<Product, ProductsDto>();
        CreateMap<Product, ProductsInListDto>();
        CreateMap<CreateUpdateProductsDto, Product>();

        // ManuFactures
        CreateMap<ManuFacturer, ManufacturerDto>();
        CreateMap<ManuFacturer, ManufacturerInListDto>();
        CreateMap<CreateUpdateManufacturerDto, ManuFacturer>();

        // ProductAttribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();
        CreateMap<CreateUpdateProductAttributeDto, ProductAttribute>();

        // Roles
        CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConst.DescriptionFieldName) ? x.ExtraProperties[RoleConst.DescriptionFieldName] : null)); 
        CreateMap<IdentityRole, RoleInListDto>().ForMember(x=>x.Description,
            map=>map.MapFrom(x=>x.ExtraProperties.ContainsKey(RoleConst.DescriptionFieldName)? x.ExtraProperties[RoleConst.DescriptionFieldName]:null));
        CreateMap<CreateUpdateRoleDto, IdentityRole>();


        // User
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityUser, UserInlistDto>();
    }
}
