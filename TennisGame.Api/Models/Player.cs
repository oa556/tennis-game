namespace TennisGame.Api.Models;

internal class Player
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required int Skill { get; init; }
}
