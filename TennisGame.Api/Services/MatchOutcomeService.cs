using TennisGame.Api.Models;
using TennisGame.Api.Persistence;
using TennisGame.Shared;

namespace TennisGame.Api.Services;

internal class MatchOutcomeService : IMatchOutcomeService
{
    private readonly IPlayerRepository _playerRepository;

    public MatchOutcomeService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task CreateMatchOutcomeAsync(CreateMatchOutcomeRequest request)
    {
        foreach (int playerId in request.Team1)
        {
            Player? player = await _playerRepository.FindAsync(playerId)
                ?? throw new ArgumentException(nameof(request.Team1));
            player.Skill += request.Result == MatchResult.Team1Wins ? 1 : -1;
            await _playerRepository.UpdateAsync(player);
        }

        foreach (int playerId in request.Team2)
        {
            Player? player = await _playerRepository.FindAsync(playerId)
                ?? throw new ArgumentException(nameof(request.Team1));
            player.Skill -= request.Result == MatchResult.Team2Wins ? 1 : -1;
            await _playerRepository.UpdateAsync(player);
        }
    }
}
