using BotGateway.Infrastructure.Api.Contracts.Player.Responses;
using BotGateway.Infrastructure.Api.Contracts.Player.Requests;
using System.Net.Http.Json;

namespace BotGateway.Infrastructure.Api.ApiClients
{
    public sealed class PlayerApiClient
    {
        private readonly HttpClient _http;

        public PlayerApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<SetupPlayerResponse?> SetupPlayerAsync(SetupPlayerRequest request)
        {
            var response = await _http.PostAsJsonAsync("/player/setup", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<SetupPlayerResponse>();
        }
    }
}
