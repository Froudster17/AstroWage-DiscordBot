using Discord.WebSocket;

namespace BotGateway.Discord.Commands;

public interface ICommand
{
    Task ExecuteAsync(SocketSlashCommand command);
}