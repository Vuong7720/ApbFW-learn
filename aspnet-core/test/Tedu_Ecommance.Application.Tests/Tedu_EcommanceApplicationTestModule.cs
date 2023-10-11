using Volo.Abp.Modularity;

namespace Tedu_Ecommance;

[DependsOn(
    typeof(Tedu_EcommanceApplicationModule),
    typeof(Tedu_EcommanceDomainTestModule)
    )]
public class Tedu_EcommanceApplicationTestModule : AbpModule
{

}
