namespace GameApi.Features.Guilds.Setup
{
    public class SetupPlayerRequest
    {
        public ulong DiscordGuildId { get; set; }

        public ulong DiscordUserId { get; set; }

        public required string Username { get; set; }
    }
}
