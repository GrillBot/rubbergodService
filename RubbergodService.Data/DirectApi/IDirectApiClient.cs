using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace RubbergodService.Data.DirectApi;

public interface IDirectApiClient
{
    /// <summary>
    /// Send message via direct API channel.
    /// </summary>
    /// <param name="channelId">ID of channel for direct API communication.</param>
    /// <param name="data">JSON serialized data</param>
    /// <param name="timeout">Timeout value in miliseconds.</param>
    /// <param name="timeoutChecks"></param>
    Task<string> SendAsync(ulong channelId, JsonDocument data, int timeout, int timeoutChecks);
}
