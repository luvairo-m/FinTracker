using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinTracker.Api.Extensions;

public static class HostExtensions
{
    public static IHost Migrate(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        
        var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            
        try
        {
            migrationRunner.MigrateUp();
        }
        catch
        {
            // ignored
        }

        return host;
    }
}