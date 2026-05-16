using Discord.WebSocket;
using BotGateway.Discord.Commands.Attributes;

namespace BotGateway.Discord.Commands.Bot_Commands
{
    [SlashCommand("ping", "Check the bot's responsiveness.")]
    public sealed class PingCommand : ICommand
    {
        public async Task ExecuteAsync(SocketSlashCommand command)
        {
            if (command.GuildId is null)
            {
                await command.RespondAsync("❌ This command can only be used in a server.", ephemeral: true);
                return;
            }

            await command.RespondAsync("Pong!");
        }
    }
}
