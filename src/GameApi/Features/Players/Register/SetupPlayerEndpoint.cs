using GameApi.Common.Interface;
using GameApi.Domain.Entities;
using GameApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Features.Guilds.Setup;

public class SetupPlayerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/player/setup", async (
            SetupPlayerRequest request,
            GameDbContext dbContext) =>
        {
            var existingPlayer = await dbContext.Players
                .FirstOrDefaultAsync(p =>
                    p.DiscordUserId == request.DiscordUserId);

            var existingGuild = await dbContext.Guilds
                .FirstOrDefaultAsync(g =>
                    g.DiscordGuildId == request.DiscordGuildId);

            if (existingGuild is null)
            {
                return Results.Problem(
                    title: "Guild not found",
                    detail: $"No guild found with DiscordGuildId {request.DiscordGuildId}, please run the command /setup to setup the discord bot",
                    statusCode: StatusCodes.Status404NotFound);
            }

            if (existingPlayer is not null)
            {
                return Results.Ok(new SetupPlayerResponse
                {
                    Created = false
                });
            }

            var player = new Player
            {
                Id = Guid.NewGuid(),
                GuildId = existingGuild?.Id ?? Guid.NewGuid(),
                Guild = existingGuild,
                DiscordUserId = request.DiscordUserId,
                Username = request.Username,
                Credits = 0,
                Xp = 0,
                CurrentLocation = new Location
                {
                    Id = Guid.NewGuid(),
                    Name = "Earth"
                },
                CreatedAtUtc = DateTime.UtcNow,
                LastSeenAtUtc = DateTime.UtcNow
            };

            dbContext.Players.Add(player);

            await dbContext.SaveChangesAsync();

            return Results.Ok(new SetupPlayerResponse
            {
                Created = true
            });
        });
    }
}