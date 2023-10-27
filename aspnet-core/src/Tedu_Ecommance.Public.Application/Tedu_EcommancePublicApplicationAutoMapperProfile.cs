using AutoMapper;
using Tedu_Ecommance.ManuFacturers;
using Tedu_Ecommance.ProductAttributes;
using Tedu_Ecommance.ProductCategories;
using Tedu_Ecommance.Products;
using Tedu_Ecommance.Public.Catalogs.Manufacturers;
using Tedu_Ecommance.Public.Catalogs.ProductCategories;
using Tedu_Ecommance.Public.Catalogs.Products;
using Tedu_Ecommance.Public.Catalogs.ProductAttributes;

namespace Tedu_Ecommance.Public;

public class Tedu_EcommancePublicApplicationAutoMapperProfile : Profile
{
    public Tedu_EcommancePublicApplicationAutoMapperProfile()
    {
        // category
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();


        // product
        CreateMap<Product, ProductsDto>();
        CreateMap<Product, ProductsInListDto>();

        // ManuFactures
        CreateMap<ManuFacturer, ManufacturerDto>();
        CreateMap<ManuFacturer, ManufacturerInListDto>();

        // ProductAttribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();
    }
}
