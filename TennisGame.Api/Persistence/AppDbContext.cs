using Microsoft.EntityFrameworkCore;
using TennisGame.Api.Models;

namespace TennisGame.Api.Persistence;

internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Player> Players { get; set; }
}
