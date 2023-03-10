using Discord;
using Discord.Rest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RubbergodService.Data.DirectApi;
using RubbergodService.Data.Discord;
using RubbergodService.Data.Entity;
using RubbergodService.Data.Managers;
using RubbergodService.Data.MemberSynchronization;
using RubbergodService.Data.Repository;

namespace RubbergodService.Data;

public static class DataExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, out string connectionString)
    {
        var connString = configuration.GetConnectionString("Default")!;
        connectionString = connString;

        return services
            .AddDbContext<RubbergodServiceContext>(opt => opt.EnableSensitiveDataLogging().EnableDetailedErrors().EnableThreadSafetyChecks().UseNpgsql(connString))
            .AddScoped<RubbergodServiceRepository>();
    }

    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services
            .AddScoped<KarmaManager>()
            .AddScoped<UserManager>()
            .AddSingleton<DiagnosticManager>();
        return services;
    }

    public static IServiceCollection AddDiscord(this IServiceCollection services)
    {
        var config = new DiscordRestConfig
        {
            LogLevel = LogSeverity.Verbose,
            FormatUsersInBidirectionalUnicode = false
        };

        var client = new DiscordRestClient(config);
        services
            .AddSingleton<IDiscordClient>(client)
            .AddSingleton<DiscordLogManager>()
            .AddScoped<IDiscordManager, DiscordManager>();
        return services;
    }

    public static IServiceCollection AddDirectApi(this IServiceCollection services)
    {
        services
            .AddSingleton<IDirectApiClient, DirectApiClient>()
            .AddScoped<DirectApiManager>();
        return services;
    }

    public static IServiceCollection AddMemberSync(this IServiceCollection services)
    {
        services
            .AddSingleton<MemberSyncQueue>()
            .AddHostedService<MemberSyncService>();
        return services;
    }
}
