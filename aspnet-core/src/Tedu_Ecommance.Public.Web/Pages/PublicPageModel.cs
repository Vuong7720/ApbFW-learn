using Tedu_Ecommance.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tedu_Ecommance.Public.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class PublicPageModel : AbpPageModel
{
    protected PublicPageModel()
    {
        LocalizationResourceType = typeof(Tedu_EcommanceResource);
    }
}
