using BotGateway.Configuration;
using BotGateway.Discord.Commands;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Reflection;
using BotGateway.Discord.Commands.Attributes;

namespace BotGateway.Discord.Interactions;

public sealed class SlashCommandRegistrar
{
    private readonly DiscordSocketClient _client;
    private readonly DiscordSettings _settings;
    private readonly ILogger<SlashCommandRegistrar> _logger;
    private readonly IEnumerable<ICommand> _commands;

    public SlashCommandRegistrar(
        DiscordSocketClient client,
        IOptions<DiscordSettings> settings,
        ILogger<SlashCommandRegistrar> logger,
        IEnumerable<ICommand> commands)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
        _commands = commands;
    }

    public async Task RegisterAsync()
    {
        var guild = _client.GetGuild(_settings.DevGuildId);

        if (guild is null)
        {
            _logger.LogWarning("Dev guild not found.");
            return;
        }

        var slashCommands = new List<ApplicationCommandProperties>();

        foreach (var command in _commands)
        {
            var attr = command.GetType()
                .GetCustomAttribute<SlashCommandAttribute>();

            if (attr is null)
                continue;

            var builder = new SlashCommandBuilder()
                .WithName(attr.Name)
                .WithDescription(attr.Description);

            slashCommands.Add(builder.Build());
        }

        await guild.BulkOverwriteApplicationCommandAsync(slashCommands.ToArray());

        _logger.LogInformation(
            "Slash commands synchronized from attributes.");
    }
}