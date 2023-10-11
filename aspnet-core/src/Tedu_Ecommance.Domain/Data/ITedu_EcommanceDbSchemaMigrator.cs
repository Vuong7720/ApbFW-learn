using System.Threading.Tasks;

namespace Tedu_Ecommance.Data;

public interface ITedu_EcommanceDbSchemaMigrator
{
    Task MigrateAsync();
}
