using System.Reflection;
using BotGateway.Discord.Commands.Attributes;

namespace BotGateway.Discord.Commands;

public sealed class CommandRegistry
{
    private readonly Dictionary<string, ICommand> _commands;

    public CommandRegistry(IEnumerable<ICommand> commands)
    {
        _commands = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase);

        foreach (var command in commands)
        {
            var attribute = command.GetType()
                .GetCustomAttribute<SlashCommandAttribute>();

            if (attribute is null)
                continue;

            _commands[attribute.Name] = command;
        }
    }

    public bool TryGet(string name, out ICommand command)
    {
        return _commands.TryGetValue(name, out command!);
    }

    public IEnumerable<(string Name, string Description)> GetMetadata()
    {
        return _commands.Select(kvp =>
        {
            var attr = kvp.Value.GetType()
                .GetCustomAttribute<SlashCommandAttribute>()!;

            return (attr.Name, attr.Description);
        });
    }
}