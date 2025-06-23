namespace Xorberax.Duel.Core.Aseprite.Models;

public readonly record struct AsepriteFrameInfo
{
    // "filename": "stickman 0.aseprite",
    // "frame": { "x": 0, "y": 0, "w": 64, "h": 64 },
    // "rotated": false,
    // "trimmed": false,
    // "spriteSourceSize": { "x": 0, "y": 0, "w": 64, "h": 64 },
    // "sourceSize": { "w": 64, "h": 64 },
    // "duration": 100
    public string Filename { get; init; }
    public AsepriteFrameRect Frame { get; init; }
    public bool Rotated { get; init; }
    public bool Trimmed { get; init; }
    public AsepriteFrameRect SpriteSourceSize { get; init; }
    public AsepriteFrameSize SourceSize { get; init; }
    public int Duration { get; init; }
}