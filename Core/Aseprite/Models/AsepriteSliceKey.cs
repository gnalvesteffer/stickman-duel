namespace Xorberax.Duel.Core.Aseprite.Models;

public readonly record struct AsepriteSliceKey
{
    public int Frame { get; init; }
    public AsepriteFrameRect Bounds { get; init; }
    public AsepriteFramePoint Center { get; init; }
    public AsepriteFramePoint Pivot { get; init; }
}