using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tedu_Ecommance.Data;
using Volo.Abp.DependencyInjection;

namespace Tedu_Ecommance.EntityFrameworkCore;

public class EntityFrameworkCoreTedu_EcommanceDbSchemaMigrator
    : ITedu_EcommanceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTedu_EcommanceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the Tedu_EcommanceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<Tedu_EcommanceDbContext>()
            .Database
            .MigrateAsync();
    }
}
