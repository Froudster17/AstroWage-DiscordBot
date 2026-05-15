namespace GameApi.Domain.Entities
{
    public class Player
    {
        public Guid Id { get; set; }

        public string DiscordUserId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
    }
}
