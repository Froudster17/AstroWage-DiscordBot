namespace GameApi.Domain.Entities
{
    public class Player
    {
        public Guid Id { get; set; }

        public Guid GuildId { get; set; }

        public required Guild Guild { get; set; }

        public ulong DiscordUserId { get; set; }

        public required string Username { get; set; }

        public long Credits { get; set; }

        public long Xp { get; set; }

        public required Location CurrentLocation { get; set; }

        public Guid? CurrentShipId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime LastSeenAtUtc { get; set; }
    }
}
