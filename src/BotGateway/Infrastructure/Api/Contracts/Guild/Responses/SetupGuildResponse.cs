using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGateway.Infrastructure.Api.Contracts.Server.Responses
{
    public sealed class SetupGuildResponse
    {
        public Guid GuildId { get; set; }

        public ulong DiscordGuildId { get; set; }

        public bool Created { get; set; }
    }
}
