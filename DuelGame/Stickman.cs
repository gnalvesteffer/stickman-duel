using System.Numerics;

namespace Xorberax.Duel.DuelGame;

public record Stickman
{
    /// <summary>
    ///    The current animation of the stickman.
    /// </summary>
    public StickmanAnimationType AnimationType { get; init; } = StickmanAnimationType.Idle;
    
    /// <summary>
    ///     The timestamp when the current animation started.
    /// </summary>
    public float AnimationStartTime { get; init; } = 0.0f;
    
    public Vector2 Position { get; init; } = Vector2.Zero;
}