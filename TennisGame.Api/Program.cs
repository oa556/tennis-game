using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TennisGame.Api.Persistence;
using TennisGame.Api.Services;

var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(Environment.GetEnvironmentVariable("DbConnectionString"));
        });
        services.AddScoped<IMatchOutcomeService, MatchOutcomeService>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.CreateDbIfNotExists();

host.Run();
