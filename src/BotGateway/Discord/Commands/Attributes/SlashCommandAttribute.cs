namespace BotGateway.Discord.Commands.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class SlashCommandAttribute : Attribute
{
    public string Name { get; }
    public string Description { get; }

    public SlashCommandAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}