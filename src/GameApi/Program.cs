using GameApi.Infrastructure.Persistence;
using GameApi.Features.Guilds.Setup;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GameApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureLogging(builder);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        ConfigureMiddleware(app);

        ConfigureEndpoints(app);

        app.Run();
    }

    private static void ConfigureLogging(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    private static void ConfigureServices(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddDbContext<GameDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("Postgres"));
        });
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }
    }

    private static void ConfigureEndpoints(WebApplication app)
    {
        app.MapSetupGuildEndpoint();


        app.MapGet("/health", () =>
        {
            return Results.Ok(new
            {
                status = "healthy"
            });
        });
    }
}