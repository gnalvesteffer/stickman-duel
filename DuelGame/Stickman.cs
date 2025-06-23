using System.Numerics;
using Raylib_cs;

namespace Xorberax.Duel.DuelGame;

public class Stickman
{
    private StickmanAnimationType _animation = StickmanAnimationType.Idle;

    /// <summary>
    ///    The current animation of the stickman.
    /// </summary>
    public StickmanAnimationType Animation
    {
        get => _animation;
        set
        {
            if (_animation == value)
            {
                return;
            }

            _animation = value;
            AnimationStartTime = Raylib.GetTime();
        }
    }

    /// <summary>
    ///     The timestamp when the current animation started.
    /// </summary>
    public double AnimationStartTime { get; private set; }

    public Vector2 Position { get; set; } = Vector2.Zero;
    public int MaxHealth { get; set; } = 10;
    public int Health { get; set; } = 10;
}