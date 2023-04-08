using TennisGame.Shared;

namespace TennisGame.Api.Services;

internal interface IMatchOutcomeService
{
    Task CreateMatchOutcomeAsync(CreateMatchOutcomeRequest request);
}
