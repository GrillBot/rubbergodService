using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RubbergodService.Data.Entity;
using RubbergodService.Data.Repository;

namespace RubbergodService.Data;

public static class DataExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        return services
            .AddDbContext<RubbergodServiceContext>(opt => opt.EnableSensitiveDataLogging().EnableDetailedErrors().EnableThreadSafetyChecks().UseNpgsql(connectionString))
            .AddScoped<RubbergodServiceRepository>();
    }
}
