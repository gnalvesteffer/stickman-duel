using System.Numerics;

namespace Xorberax.Duel.DuelGame;

public class Stickman
{
    /// <summary>
    ///    The current animation of the stickman.
    /// </summary>
    public StickmanAnimationType AnimationType { get; set; } = StickmanAnimationType.Idle;
    
    /// <summary>
    ///     The timestamp when the current animation started.
    /// </summary>
    public float AnimationStartTime { get; set; } = 0.0f;
    
    public Vector2 Position { get; set; } = Vector2.Zero;
}