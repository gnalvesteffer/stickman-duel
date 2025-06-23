namespace Xorberax.Duel.Core.Aseprite.Models;

public readonly record struct AsepriteFrameTag
{
    public string Name { get; init; }
    public int From { get; init; }
    public int To { get; init; }
    public string Direction { get; init; }
    public string Color { get; init; }

    /// <summary>
    ///     Number of times to play the animation. If null, the animation should loop indefinitely.
    /// </summary>
    public int? Repeat { get; init; }

    public bool ShouldLoop => Repeat is null;
}