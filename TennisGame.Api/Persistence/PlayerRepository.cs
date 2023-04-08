using Microsoft.EntityFrameworkCore;
using TennisGame.Api.Models;

namespace TennisGame.Api.Persistence;

internal class PlayerRepository : IPlayerRepository
{
    private readonly AppDbContext _dbContext;

    public PlayerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Player[]> GetAllAsync()
    {
        return await _dbContext.Players.ToArrayAsync();
    }

    public async Task<Player?> FindAsync(int id)
    {
        return await _dbContext.Players.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(Player player)
    {
        _dbContext.Players.Update(player);
        await _dbContext.SaveChangesAsync();
    }
}
