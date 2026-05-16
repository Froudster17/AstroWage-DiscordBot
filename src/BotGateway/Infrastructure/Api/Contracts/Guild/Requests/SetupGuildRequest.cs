using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGateway.Infrastructure.Api.Contracts.Server.Requests
{
    public sealed class SetupGuildRequest
    {
        public string? DiscordGuildId { get; set; }
    }
}
