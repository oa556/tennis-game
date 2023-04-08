using System.Text;

namespace TennisGame.Shared;

public record CreateMatchOutcomeRequest(int[] Team1, int[] Team2, MatchResult Result);
