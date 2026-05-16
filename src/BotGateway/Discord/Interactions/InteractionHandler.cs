using BotGateway.Discord.Commands;
using Discord.WebSocket;
using System.Windows.Input;
using ICommand = BotGateway.Discord.Commands.ICommand;

namespace BotGateway.Discord.Interactions;

public sealed class InteractionHandler
{
    private readonly ILogger<InteractionHandler> _logger;
    private readonly CommandRegistry _registry;

    public InteractionHandler(
        ILogger<InteractionHandler> logger,
        CommandRegistry registry)
    {
        _logger = logger;
        _registry = registry;
    }

    public async Task HandleAsync(SocketInteraction interaction)
    {
        if (interaction is not SocketSlashCommand command)
            return;

        if (!_registry.TryGet(command.Data.Name, out var cmd))
        {
            await command.RespondAsync("Unknown command.", ephemeral: true);
            return;
        }

        await cmd.ExecuteAsync(command);
    }
}