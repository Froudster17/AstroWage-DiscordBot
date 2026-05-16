using BotGateway.Discord.Commands.Attributes;
using BotGateway.Infrastructure.Api.ApiClients;
using BotGateway.Infrastructure.Api.Contracts.Server.Requests;
using Discord;
using Discord.WebSocket;

namespace BotGateway.Discord.Commands.Server_Commands
{
    [SlashCommand("setup_bot", "Setup the bot for your server")]
    public sealed class SetupCommand : ICommand
    {
        private readonly GuildApiClient _guildApi;

        public SetupCommand(GuildApiClient guildApi)
        {
            _guildApi = guildApi;
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

            var result = await _guildApi.SetupGuildAsync(request);

            if (result is null)
            {
                await command.RespondAsync("❌ Setup failed.", ephemeral: true);
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("AstroWage Setup")
                .WithColor(result.Created ? Color.Green : Color.Blue)
                .WithDescription(result.Created
                    ? "Server successfully created and registered in the system."
                    : "This server was already set up.")
                .AddField("Guild ID", command.GuildId.Value, inline: false)
                .AddField("Status", result.Created ? "Created" : "Already Exists", inline: true)
                .Build();

            await command.RespondAsync(embed: embed, ephemeral: true);
        }
    }
}
