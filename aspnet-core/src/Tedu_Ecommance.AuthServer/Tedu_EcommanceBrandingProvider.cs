using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Tedu_Ecommance;

[Dependency(ReplaceServices = true)]
public class Tedu_EcommanceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Tedu_Ecommance";
}
