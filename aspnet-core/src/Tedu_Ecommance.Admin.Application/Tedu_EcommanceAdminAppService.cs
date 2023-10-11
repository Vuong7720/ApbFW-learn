using System;
using System.Collections.Generic;
using System.Text;
using Tedu_Ecommance.Localization;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Admin;

/* Inherit your application services from this class.
 */
public abstract class Tedu_EcommanceAdminAppService : ApplicationService
{
    protected Tedu_EcommanceAdminAppService()
    {
        LocalizationResource = typeof(Tedu_EcommanceResource);
    }
}
