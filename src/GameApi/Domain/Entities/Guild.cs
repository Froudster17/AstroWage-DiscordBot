namespace GameApi.Domain.Entities
{
    public class Guild
    {
        public Guid Id { get; set; }

        public string DiscordGuildId { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
    }
}
