using BotGateway.Discord.Commands.Attributes;
using BotGateway.Infrastructure.Api.ApiClients;
using BotGateway.Infrastructure.Api.Contracts.Server.Requests;
using Discord.WebSocket;

namespace BotGateway.Discord.Commands.Server_Commands
{
    [SlashCommand("setup", "Setup the bot for your server")]
    public sealed class SetupCommand : ICommand
    {
        private readonly GuildApiClient _serverApi;

        public SetupCommand(GuildApiClient serverApi)
        {
            _serverApi = serverApi;
        }

        public async Task ExecuteAsync(SocketSlashCommand command)
        {
            if (command.GuildId is null)
            {
                await command.RespondAsync("❌ This command can only be used in a server.", ephemeral: true);
                return;
            }


            var request = new SetupGuildRequest
            {
                DiscordGuildId = command.GuildId.Value
            };

            var result = await _serverApi.SetupGuildAsync(request);

            if (result is null)
            {
                await command.RespondAsync("❌ Setup failed.", ephemeral: true);
                return;
            }

            var message = result.Created
                ? "✅ Server created successfully!"
                : "ℹ️ Server already exists.";

            await command.RespondAsync(message, ephemeral: true);
        }
    }
}
