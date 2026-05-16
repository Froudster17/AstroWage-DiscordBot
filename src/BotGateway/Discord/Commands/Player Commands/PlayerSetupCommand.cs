using BotGateway.Discord.Commands.Attributes;
using BotGateway.Infrastructure.Api.ApiClients;
using BotGateway.Infrastructure.Api.Contracts.Player.Requests;
using Discord;
using Discord.WebSocket;

namespace BotGateway.Discord.Commands.Player_Commands
{
    [SlashCommand("setup_profile", "Setup your player account")]
    public sealed class PlayerSetupCommand : ICommand
    {
        private readonly PlayerApiClient _playerApi;

        public PlayerSetupCommand(PlayerApiClient playerApi)
        {
            _playerApi = playerApi;
        }

        public async Task ExecuteAsync(SocketSlashCommand command)
        {
            if (command.GuildId is null)
            {
                await command.RespondAsync("❌ This command can only be used in a server.", ephemeral: true);
                return;
            }

            var request = new SetupPlayerRequest
            {
                DiscordGuildId = command.GuildId.Value,
                DiscordUserId = command.User.Id,
                Username = command.User.Username
            };

            var result = await _playerApi.SetupPlayerAsync(request);

            if (result is null)
            {
                await command.RespondAsync("❌ Setup failed.", ephemeral: true);
                return;
            }

            var embed = new EmbedBuilder();

            if (result.Created)
            {
                embed.WithTitle("✅ Player Setup Successful")
                     .WithDescription("Your player account has been created successfully.")
                     .WithColor(Color.Green);
            }
            else
            {
                embed.WithTitle("⚠️ Player Already Exists")
                     .WithDescription("You already have a player account set up.")
                     .WithColor(Color.Orange);
            }

            await command.RespondAsync(embed: embed.Build(), ephemeral: true);
        }
    }
}
