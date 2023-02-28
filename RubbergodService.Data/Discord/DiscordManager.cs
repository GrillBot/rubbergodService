using Discord;
using Discord.Rest;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace RubbergodService.Data.Discord;

public class DiscordManager : IDiscordManager
{
    private IDiscordClient Client { get; }
    private IConfiguration Configuration { get; }
    private IMemoryCache MemoryCache { get; }

    public DiscordManager(IDiscordClient client, IConfiguration configuration, IMemoryCache memoryCache)
    {
        Client = client;
        Configuration = configuration;
        MemoryCache = memoryCache;
    }

    public async Task LoginAsync()
    {
        var token = Configuration.GetConnectionString("DiscordBot") ?? throw new ArgumentNullException(nameof(Configuration));
        await ((DiscordRestClient)Client).LoginAsync(TokenType.Bot, token);
    }

    public async Task<IUser?> GetUserAsync(ulong id)
    {
        var cacheKey = $"User_{id}";
        if (MemoryCache.TryGetValue(cacheKey, out IUser? user))
            return user;

        user = await Client.GetUserAsync(id);
        if (user != null)
            MemoryCache.Set(cacheKey, user, DateTimeOffset.Now.AddSeconds(30));

        return user;
    }
}
