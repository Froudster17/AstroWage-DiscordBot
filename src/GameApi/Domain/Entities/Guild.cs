namespace GameApi.Domain.Entities
{
    public class Guild
    {
        public Guid Id { get; set; }

        public ulong DiscordGuildId { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
