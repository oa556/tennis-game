using TennisGame.Shared;

namespace TennisGame.Client.Services;

public interface IDataService
{
    Task<PlayerDto> GetPlayerAsync(int id);
    Task<PlayerDto[]> GetPlayersAsync();
    Task CreateMatchOutcomeAsync(CreateMatchOutcomeRequest request);
}
