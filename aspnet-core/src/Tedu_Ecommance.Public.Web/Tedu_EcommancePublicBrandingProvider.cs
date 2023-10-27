using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Tedu_Ecommance.Public.Web;

[Dependency(ReplaceServices = true)]
public class Tedu_EcommancePublicBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Public";
}
