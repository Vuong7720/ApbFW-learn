using Tedu_Ecommance.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tedu_Ecommance.Admin.Permissions;

public class Tedu_EcommancePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(Tedu_EcommancePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(Tedu_EcommancePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Tedu_EcommanceResource>(name);
    }
}
