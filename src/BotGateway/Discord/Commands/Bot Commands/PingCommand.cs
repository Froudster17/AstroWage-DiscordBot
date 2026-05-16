using Discord.WebSocket;
using BotGateway.Discord.Commands.Attributes;

namespace BotGateway.Discord.Commands.Bot_Commands
{
    [SlashCommand("ping", "Check the bot's responsiveness.")]
    public sealed class PingCommand : ICommand
    {
        public string Name => "ping";

        public async Task ExecuteAsync(SocketSlashCommand command)
        {
            await command.RespondAsync("Pong!");
        }
    }
}
