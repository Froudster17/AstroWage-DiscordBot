namespace BotGateway.Configuration;

public sealed class DiscordSettings
{
    public const string SectionName = "Discord";

    public string Token { get; set; } = string.Empty;

    public string Prefix { get; set; } = "!";

    public ulong DevGuildId { get; set; }
}