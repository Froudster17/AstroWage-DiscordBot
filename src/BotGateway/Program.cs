using BotGateway.Configuration;
using BotGateway.Discord.Commands;
using BotGateway.Discord.HostedServices;
using BotGateway.Discord.Interactions;
using BotGateway.Infrastructure.Api.ApiClients;
using Discord;
using Discord.WebSocket;
using DotNetEnv;
using Microsoft.Extensions.Options;
using Serilog;

namespace BotGateway;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Env.Load();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            Log.Information("Starting BotGateway...");

            var host = Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) =>
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .WriteTo.Console();
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context, services);
                })
                .Build();

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "BotGateway terminated unexpectedly.");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static void ConfigureServices(
        HostBuilderContext context,
        IServiceCollection services)
    {
        services.Configure<DiscordSettings>(
            context.Configuration.GetSection(DiscordSettings.SectionName));

        services.Configure<GameApiSettings>(
            context.Configuration.GetSection(GameApiSettings.SectionName));



        services.AddSingleton(sp =>
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents =
                    GatewayIntents.Guilds |
                    GatewayIntents.GuildMessages |
                    GatewayIntents.MessageContent,

                AlwaysDownloadUsers = false
            };

            return new DiscordSocketClient(config);
        });

        services.AddHttpClient<GameApiClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<GameApiSettings>>().Value;

            client.BaseAddress = new Uri(settings.BaseUrl);
        });

        services.AddHttpClient<GuildApiClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<GameApiSettings>>().Value;

            client.BaseAddress = new Uri(settings.BaseUrl);
        });

        services.AddHttpClient<PlayerApiClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<GameApiSettings>>().Value;

            client.BaseAddress = new Uri(settings.BaseUrl);
        });

        services.AddHostedService<DiscordBotHostedService>();
        services.AddSingleton<InteractionHandler>();
        services.AddSingleton<SlashCommandRegistrar>();

        services.AddSingleton<CommandRegistry>();

        services.Scan(scan => scan
            .FromAssemblyOf<ICommand>()
            .AddClasses(c => c.AssignableTo<ICommand>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }
}