namespace Xorberax.Duel.Core.Aseprite.Models;

public readonly record struct AsepriteAnimationInfo
{
    public AsepriteFrameInfo[] Frames { get; init; }
    public AsepriteMetadata Meta { get; init; }
    public int FrameRate => 1000 / Frames.FirstOrDefault().Duration;
}