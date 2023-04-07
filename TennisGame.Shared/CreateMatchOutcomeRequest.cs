using System.Text;

namespace TennisGame.Shared;

public record CreateMatchOutcomeRequest(int[] Team1, int[] Team2, MatchResult Result)
{
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append("{ Team 1: ");
        foreach (var player in Team1)
        {
            builder.Append($"{player.ToString()}, ");
        }

        builder.Append("Team 2: ");
        foreach (var player in Team2)
        {
            builder.Append($"{player.ToString()}, ");
        }

        builder.Append($"Result: {Result} }}");

        return builder.ToString();
    }
}
