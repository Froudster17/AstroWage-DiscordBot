using BotGateway.Infrastructure.Api.Contracts.Player.Responses;
using BotGateway.Infrastructure.Api.Contracts.Player.Requests;
using System.Net.Http.Json;

namespace BotGateway.Infrastructure.Api.ApiClients;

public sealed class GameApiClient
{
    private readonly HttpClient _http;

    public GameApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<SetupPlayerResponse?> SetupPlayerAsync(SetupPlayerRequest request)
    {
        var response = await _http.PostAsJsonAsync("/setup", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<SetupPlayerResponse>();
    }
}