namespace GameApi.Domain.Entities
{
    public class Player
    {
        public Guid Id { get; set; }

        public Guid GuildId { get; set; }

        public Guild Guild { get; set; } = null!;

        public ulong DiscordUserId { get; set; }

        public string Username { get; set; } = null!;

        public long Credits { get; set; }

        public long Xp { get; set; }

        public Guid CurrentLocationId { get; set; }

        public Location CurrentLocation { get; set; } = null!;

        public Guid? CurrentShipId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime LastSeenAtUtc { get; set; }
    }
}
