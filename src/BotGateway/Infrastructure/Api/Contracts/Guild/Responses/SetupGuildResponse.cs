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

        public string DiscordGuildId { get; set; } = string.Empty;

        public bool Created { get; set; }
    }
}
