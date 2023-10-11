using Volo.Abp.Modularity;

namespace Tedu_Ecommance.Admin;

[DependsOn(
    typeof(Tedu_EcommanceAdminApplicationModule),
    typeof(Tedu_EcommanceDomainTestModule)
    )]
public class Tedu_EcommanceApplicationTestModule : AbpModule
{

}
