using Discord;
using Discord.Rest;
using Microsoft.Extensions.Configuration;

namespace RubbergodService.Data.Discord;

public class DiscordManager : IDiscordManager
{
    private IDiscordClient Client { get; }
    private IConfiguration Configuration { get; }

    public DiscordManager(IDiscordClient client, IConfiguration configuration)
    {
        Client = client;
        Configuration = configuration;
    }

    public async Task LoginAsync()
    {
        var token = Configuration.GetConnectionString("DiscordBot") ?? throw new ArgumentNullException(nameof(Configuration));
        await ((DiscordRestClient)Client).LoginAsync(TokenType.Bot, token);
    }

    public Task<IUser?> GetUserAsync(ulong id)
    {
        return Client.GetUserAsync(id);
    }
}
