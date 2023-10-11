using Tedu_Ecommance.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tedu_Ecommance.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Tedu_EcommanceController : AbpControllerBase
{
    protected Tedu_EcommanceController()
    {
        LocalizationResource = typeof(Tedu_EcommanceResource);
    }
}
