using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TennisGame.Api.Models;

namespace TennisGame.Api.Persistence;

internal static class DbInitializer
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try
        {
            dbContext.Database.EnsureCreated();
            if (dbContext.Players.Any())
            {
                return;
            }
            dbContext.Players.AddRange(CreatePlayers());
            dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger(nameof(DbInitializer));
            logger.LogError(ex, "An error occured while creating the database");
        }
    }

    private static IEnumerable<Player> CreatePlayers()
    {
        yield return new Player
        {
            Name = $"John",
            Skill = 2
        };
        yield return new Player
        {
            Name = $"Michael",
            Skill = 3
        };
        yield return new Player
        {
            Name = $"Rachel",
            Skill = 7
        };
        yield return new Player
        {
            Name = $"Phill",
            Skill = 4
        };
    }
}
