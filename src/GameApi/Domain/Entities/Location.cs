using GameApi.Common.Enum;

namespace GameApi.Domain.Entities
{
    public class Location
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public LocationType Type { get; set; }

        public int DangerLevel { get; set; }
    }
}
