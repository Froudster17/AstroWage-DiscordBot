using GameApi.Common.Interface;
using GameApi.Domain.Entities;
using GameApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Features.Guilds.Setup;

public class SetupGuildEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/guilds/setup", async (
            SetupGuildRequest request,
            GameDbContext dbContext) =>
        {
            var existingGuild = await dbContext.Guilds
                .FirstOrDefaultAsync(g =>
                    g.DiscordGuildId == request.DiscordGuildId);

            if (existingGuild is not null)
            {
                return Results.Ok(new SetupGuildResponse
                {
                    GuildId = existingGuild.Id,
                    DiscordGuildId = existingGuild.DiscordGuildId,
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