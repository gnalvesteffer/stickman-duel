namespace Xorberax.Duel.DuelGame;

public record DuelGameState
{
    public Stickman PlayerStickman { get; set; } = new();
    public Stickman OpponentStickman { get; set; } = new();
}