namespace BotGateway.Configuration;

public sealed class GameApiSettings
{
    public const string SectionName = "GameApi";

    public string BaseUrl { get; set; } = string.Empty;
}