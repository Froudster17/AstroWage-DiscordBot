using GameApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Infrastructure.Persistence
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players => Set<Player>();

        public DbSet<Guild> Guilds => Set<Guild>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}