using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RubbergodService.Data.Models.DirectApi;

namespace RubbergodService.Data.DirectApi;

public class DirectApiManager
{
    private IConfiguration Configuration { get; }
    private IDirectApiClient Client { get; }

    public DirectApiManager(IConfiguration configuration, IDirectApiClient client)
    {
        Configuration = configuration.GetRequiredSection("DirectApi");
        Client = client;
    }

    public async Task<JsonDocument> SendAsync(string service, DirectApiCommand command)
    {
        var configuration = Configuration.GetRequiredSection(service);
        var channelId = configuration.GetValue<ulong>("ChannelId");
        var timeout = configuration.GetValue<int>("Timeout");
        var timeoutChecks = configuration.GetValue<int>("Checks");

        using var jsonCommand = JsonSerializer.SerializeToDocument(command);
        var response = await Client.SendAsync(channelId, jsonCommand, timeout, timeoutChecks);
        return JsonDocument.Parse(response);
    }
}
