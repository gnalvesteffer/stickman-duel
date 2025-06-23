namespace Xorberax.Duel.Core.Aseprite.Models;

public readonly record struct AsepriteFrameRect
{
    public int X { get; init; }
    public int Y { get; init; }
    public int W { get; init; }
    public int H { get; init; }
}