using GameApi.Domain.Entities;
using GameApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Features.Guilds.Setup
{
    public static class SetupGuildEndpoint
    {
        public static void MapSetupGuildEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/guilds/setup", async (SetupGuildRequest request, GameDbContext dbContext) =>
            {
                var exisitingGuild = await dbContext.Guilds.FirstOrDefaultAsync(g => g.DiscordGuildId == request.DiscordGuildId);

                if (exisitingGuild != null)
                {
                    return Results.Ok(new SetupGuildResponse
                    {
                        GuildId = exisitingGuild.Id,
                        DiscordGuildId = exisitingGuild.DiscordGuildId,
                        Created = false
                    });
                }

                var guild = new Guild
                {
                    Id = Guid.NewGuid(),
                    DiscordGuildId = request.DiscordGuildId,
                    CreatedAtUtc = DateTime.UtcNow
                };

                dbContext.Guilds.Add(guild);

                await dbContext.SaveChangesAsync();

                return Results.Ok(new SetupGuildResponse
                {
                    GuildId = guild.Id,
                    DiscordGuildId = guild.DiscordGuildId,
                    Created = true
                });
            });
        }
    }
}
