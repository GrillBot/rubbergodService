using Discord;

namespace RubbergodService.Data.Discord;

public interface IDiscordManager
{
    Task LoginAsync();
    Task<IUser?> GetUserAsync(ulong id);
}
