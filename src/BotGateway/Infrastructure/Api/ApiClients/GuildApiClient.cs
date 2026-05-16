using BotGateway.Infrastructure.Api.Contracts.Server.Responses;
using BotGateway.Infrastructure.Api.Contracts.Server.Requests;
using System.Net.Http.Json;

namespace BotGateway.Infrastructure.Api.ApiClients;

public sealed class GuildApiClient
{
    private readonly HttpClient _http;

    public GuildApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<SetupGuildResponse?> SetupGuildAsync(SetupGuildRequest request)
    {
        var response = await _http.PostAsJsonAsync("/guilds/setup", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<SetupGuildResponse>();
    }
}