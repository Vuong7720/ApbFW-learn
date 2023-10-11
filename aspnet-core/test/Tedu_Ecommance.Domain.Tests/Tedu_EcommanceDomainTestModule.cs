using Tedu_Ecommance.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tedu_Ecommance;

[DependsOn(
    typeof(Tedu_EcommanceEntityFrameworkCoreTestModule)
    )]
public class Tedu_EcommanceDomainTestModule : AbpModule
{

}
