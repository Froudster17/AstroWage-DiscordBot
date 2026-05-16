using BotGateway.Configuration;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BotGateway.Discord.HostedServices;

public sealed class DiscordBotHostedService : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly DiscordSettings _settings;
    private readonly ILogger<DiscordBotHostedService> _logger;

    public DiscordBotHostedService(
        DiscordSocketClient client,
        IOptions<DiscordSettings> settings,
        ILogger<DiscordBotHostedService> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Discord bot...");

        _client.Log += OnDiscordLog;
        _client.Ready += OnReady;
        _client.Disconnected += OnDisconnected;

        await _client.LoginAsync(
            TokenType.Bot,
            _settings.Token);

        await _client.StartAsync();

        _logger.LogInformation("Discord client startup completed.");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Discord bot...");

        await _client.StopAsync();

        _logger.LogInformation("Discord bot stopped.");
    }

    private Task OnReady()
    {
        _logger.LogInformation(
            "Discord bot connected as {Username}",
            _client.CurrentUser.Username);

        return Task.CompletedTask;
    }

    private Task OnDisconnected(Exception? ex)
    {
        _logger.LogWarning(ex, "Discord client disconnected.");

        return Task.CompletedTask;
    }

    private Task OnDiscordLog(LogMessage message)
    {
        var logLevel = message.Severity switch
        {
            LogSeverity.Critical => LogLevel.Critical,
            LogSeverity.Error => LogLevel.Error,
            LogSeverity.Warning => LogLevel.Warning,
            LogSeverity.Info => LogLevel.Information,
            LogSeverity.Verbose => LogLevel.Debug,
            LogSeverity.Debug => LogLevel.Debug,
            _ => LogLevel.Information
        };

        _logger.Log(
            logLevel,
            message.Exception,
            "[Discord] {Message}",
            message.Message);

        return Task.CompletedTask;
    }
}