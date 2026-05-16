using Discord.WebSocket;
using BotGateway.Discord.Commands.Attributes;

namespace BotGateway.Discord.Commands.Player_Commands
{
    [SlashCommand("profile", "View your profile information.")]
    public sealed class ProfileCommand : ICommand
    {
        public Task ExecuteAsync(SocketSlashCommand command)
        {
            var username = command.User.Username;
            return command.RespondAsync($"Your profile: {username}");
        }
    }
}
