using GameApi.Common.Enum;

namespace GameApi.Domain.Entities
{
    public class PlayerInventory
    {
        public Guid Id { get; set; }

        public Guid GuildId { get; set; }

        public Guild Guild { get; set; } = null!;

        public Guid PlayerId { get; set; }

        public Player Player { get; set; } = null!;

        public Guid ItemId { get; set; }

        public Item Item { get; set; } = null!;

        public int Quantity { get; set; }

        public InventoryStorageType StorageType { get; set; }

        public Location Location { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
    }
}
