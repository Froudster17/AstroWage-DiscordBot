namespace BotGateway.Infrastructure.Api.Contracts.Player.Requests
{
    public sealed class SetupPlayerRequest
    {
        public string DiscordUserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
