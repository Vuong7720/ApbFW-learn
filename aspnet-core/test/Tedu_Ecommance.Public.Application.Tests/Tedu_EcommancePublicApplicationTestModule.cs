using Volo.Abp.Modularity;

namespace Tedu_Ecommance.Public;

[DependsOn(
    typeof(Tedu_EcommancePublicApplicationModule),
    typeof(Tedu_EcommanceDomainTestModule)
    )]
public class Tedu_EcommancePublicApplicationTestModule : AbpModule
{

}
