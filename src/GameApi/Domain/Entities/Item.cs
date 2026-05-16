using GameApi.Common.Enum;

namespace GameApi.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ItemType Type { get; set; }

        public bool Stackable { get; set; }

        public long BaseValue { get; set; }

        public decimal Weight { get; set; }
    }
}
