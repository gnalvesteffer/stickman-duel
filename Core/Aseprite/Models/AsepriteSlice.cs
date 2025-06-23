namespace Xorberax.Duel.Core.Aseprite.Models;

public readonly record struct AsepriteSlice
{
    public string Name { get; init; }
    public int Color { get; init; }
    public AsepriteSliceKey[] Keys { get; init; }
}