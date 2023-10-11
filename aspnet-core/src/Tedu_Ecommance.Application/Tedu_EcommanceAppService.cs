using System;
using System.Collections.Generic;
using System.Text;
using Tedu_Ecommance.Localization;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance;

/* Inherit your application services from this class.
 */
public abstract class Tedu_EcommanceAppService : ApplicationService
{
    protected Tedu_EcommanceAppService()
    {
        LocalizationResource = typeof(Tedu_EcommanceResource);
    }
}
