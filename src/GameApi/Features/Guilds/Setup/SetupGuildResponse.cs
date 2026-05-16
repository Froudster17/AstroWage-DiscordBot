namespace GameApi.Features.Guilds.Setup
{
    public class SetupGuildResponse
    {
        public Guid GuildId { get; set; }

        public ulong DiscordGuildId { get; set; }

        public bool Created { get; set; }
    }
}
