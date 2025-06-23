namespace Xorberax.Duel.Core.Aseprite.Models;

public readonly record struct AsepriteLayer
{
    public string Name { get; init; }
    public int Opacity { get; init; }
    public string BlendMode { get; init; }
}