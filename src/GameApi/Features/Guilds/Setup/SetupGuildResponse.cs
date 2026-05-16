namespace GameApi.Features.Guilds.Setup
{
    public class SetupGuildResponse
    {
        public Guid GuildId { get; set; }

        public string DiscordGuildId { get; set; } = null!;

        public bool Created { get; set; }
    }
}
