using Tedu_Ecommance.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tedu_Ecommance.Admin.Permissions;

public class Tedu_EcommancePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Define your own permissions here. Example:
        //------------Catalog
        var catalogGroup = context.AddGroup(Tedu_EcommancePermissions.CatalogGroupName);
        //Define your own permissions here. Example:

        //Add product
        var productPermistion = catalogGroup.AddPermission(Tedu_EcommancePermissions.Product.Default, L("Permission:Catalog.Product"));
        productPermistion.AddChild(Tedu_EcommancePermissions.Product.Create, L("Permission:Catalog.Product.Create"));
        productPermistion.AddChild(Tedu_EcommancePermissions.Product.Update, L("Permission:Catalog.Product.Update"));
        productPermistion.AddChild(Tedu_EcommancePermissions.Product.Delete, L("Permission:Catalog.Product.Delete"));
        productPermistion.AddChild(Tedu_EcommancePermissions.Product.AttributeManage, L("Permission:Catalog.Product.AttributeManage"));

        //Add manufacture
        var manufacturePermission = catalogGroup.AddPermission(Tedu_EcommancePermissions.Manufacture.Default, L("Permission:Catalog.Manufacture"));
        manufacturePermission.AddChild(Tedu_EcommancePermissions.Manufacture.Create, L("Permission:Catalog.Manufacture.Create"));
        manufacturePermission.AddChild(Tedu_EcommancePermissions.Manufacture.Update, L("Permission:Catalog.Manufacture.Update"));
        manufacturePermission.AddChild(Tedu_EcommancePermissions.Manufacture.Delete, L("Permission:Catalog.Manufacture.Delete"));

        //Add category
        var categoriPermission = catalogGroup.AddPermission(Tedu_EcommancePermissions.Category.Default, L("Permission:Catalog.Category"));
        categoriPermission.AddChild(Tedu_EcommancePermissions.Category.Create, L("Permission:Catalog.Category.Create"));
        categoriPermission.AddChild(Tedu_EcommancePermissions.Category.Update, L("Permission:Catalog.Category.Update"));
        categoriPermission.AddChild(Tedu_EcommancePermissions.Category.Delete, L("Permission:Catalog.Category.Delete"));

        //Add attribute
        var attributePermistion = catalogGroup.AddPermission(Tedu_EcommancePermissions.Attribute.Default, L("Permission:Catalog.Attribute"));
        attributePermistion.AddChild(Tedu_EcommancePermissions.Attribute.Create, L("Permission:Catalog.Attribute.Create"));
        attributePermistion.AddChild(Tedu_EcommancePermissions.Attribute.Update, L("Permission:Catalog.Attribute.Update"));
        attributePermistion.AddChild(Tedu_EcommancePermissions.Attribute.Delete, L("Permission:Catalog.Attribute.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Tedu_EcommanceResource>(name);
    }
}
