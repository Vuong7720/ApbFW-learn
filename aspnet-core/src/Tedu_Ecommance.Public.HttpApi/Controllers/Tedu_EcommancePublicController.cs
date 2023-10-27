using Tedu_Ecommance.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tedu_Ecommance.Public.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Tedu_EcommancePublicController : AbpControllerBase
{
    protected Tedu_EcommancePublicController()
    {
        LocalizationResource = typeof(Tedu_EcommanceResource);
    }
}
