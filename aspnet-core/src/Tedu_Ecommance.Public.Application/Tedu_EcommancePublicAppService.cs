using Tedu_Ecommance.Localization;
using Volo.Abp.Application.Services;

namespace TeduEcommerce.Public;

/* Inherit your application services from this class.
 */
public abstract class Tedu_EcommancePublicAppService : ApplicationService
{
    protected Tedu_EcommancePublicAppService()
    {
        LocalizationResource = typeof(Tedu_EcommanceResource);
    }
}