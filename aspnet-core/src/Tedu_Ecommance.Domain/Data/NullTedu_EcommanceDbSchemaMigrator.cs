using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tedu_Ecommance.Data;

/* This is used if database provider does't define
 * ITedu_EcommanceDbSchemaMigrator implementation.
 */
public class NullTedu_EcommanceDbSchemaMigrator : ITedu_EcommanceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
