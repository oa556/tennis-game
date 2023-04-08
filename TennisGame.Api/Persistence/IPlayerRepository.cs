using TennisGame.Api.Models;

namespace TennisGame.Api.Persistence;

internal interface IPlayerRepository
{
    Task<Player?> FindAsync(int id);
    Task<Player[]> GetAllAsync();
    Task UpdateAsync(Player player);
}
