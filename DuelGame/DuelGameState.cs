namespace Xorberax.Duel.DuelGame;

public class DuelGameState
{
    public Stickman PlayerStickman { get; init; } = new();
    public Stickman OpponentStickman { get; init; } = new();
}