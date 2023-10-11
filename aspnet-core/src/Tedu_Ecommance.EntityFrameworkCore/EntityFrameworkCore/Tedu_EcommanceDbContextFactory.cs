using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tedu_Ecommance.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class Tedu_EcommanceDbContextFactory : IDesignTimeDbContextFactory<Tedu_EcommanceDbContext>
{
    public Tedu_EcommanceDbContext CreateDbContext(string[] args)
    {
        Tedu_EcommanceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<Tedu_EcommanceDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new Tedu_EcommanceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Tedu_Ecommance.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
