namespace TennisGame.Api.Models;

internal class Player
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required int Skill { get; set; }
}
