namespace Tedu_Ecommance.Admin.Permissions;

public static class Tedu_EcommancePermissions
{
    public const string SystemGroupName = "Tedu_EcomanceSystem";
    public const string CatalogGroupName = "Tedu_EcomanceCatalog";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".Tedu_EcommanceAdminSystem";


    public static class Product
    {
        public const string Default = CatalogGroupName + ".Product";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string AttributeManage = Default + ".Attribute";
    }

    public static class Attribute
    {
        public const string Default = CatalogGroupName + ".Attribute";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}
