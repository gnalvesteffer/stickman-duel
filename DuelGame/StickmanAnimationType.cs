namespace Xorberax.Duel.DuelGame;

/// <summary>
///     The animation types for the stickman. Corresponds to the labels in the <c>stickman.aseprite</c> asset.
/// </summary>
public enum StickmanAnimationType
{
    Idle,
    AttackHigh,
    AttackMiddle,
    AttackLow,
    BlockHigh,
    BlockMiddle,
    BlockLow,
}

public static class StickmanAnimationTypeExtensions
{
    /// <summary>
    ///     Converts a <see cref="StickmanAnimationType"/> to the corresponding Aseprite frame tag.
    /// </summary>
    /// <param name="animationType">The animation type to convert.</param>
    /// <returns>The corresponding Aseprite frame tag.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the animation type is not recognized.</exception>
    public static string ToAsepriteFrameTagName(this StickmanAnimationType animationType) => animationType switch
    {
        StickmanAnimationType.Idle => "idle",
        StickmanAnimationType.AttackHigh => "attack-high",
        StickmanAnimationType.AttackMiddle => "attack-middle",
        StickmanAnimationType.AttackLow => "attack-low",
        StickmanAnimationType.BlockHigh => "block-high",
        StickmanAnimationType.BlockMiddle => "block-middle",
        StickmanAnimationType.BlockLow => "block-low",
        _ => throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null)
    };
}