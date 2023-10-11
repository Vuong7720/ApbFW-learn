using Tedu_Ecommance.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace Tedu_Ecommance.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(Tedu_EcommanceEntityFrameworkCoreModule),
    typeof(Tedu_EcommanceApplicationContractsModule)
    )]
public class Tedu_EcommanceDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Tedu_Ecommance:"; });
    }
}
