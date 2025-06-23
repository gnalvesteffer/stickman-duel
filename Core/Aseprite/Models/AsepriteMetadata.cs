namespace Xorberax.Duel.Core.Aseprite.Models;

public readonly record struct AsepriteMetadata()
{
    private readonly Dictionary<string, AsepriteFrameTag> _frameTagDictionary = new();
    private readonly AsepriteFrameTag[] _frameTags = new AsepriteFrameTag[] { };

    public string App { get; init; } = null;
    public string Version { get; init; } = null;
    public string Image { get; init; } = null;
    public string Format { get; init; } = null;
    public AsepriteFrameSize Size { get; init; } = default;
    public string Scale { get; init; } = null;
    public AsepriteLayer[] Layers { get; init; } = new AsepriteLayer[] { };
    public AsepriteSlice[] Slices { get; init; } = new AsepriteSlice[] { };

    public AsepriteFrameTag[] FrameTags
    {
        get => _frameTags;
        init
        {
            _frameTags = value;
            _frameTagDictionary.Clear();
            foreach (var tag in value)
            {
                _frameTagDictionary[tag.Name] = tag;
            }
        }
    }

    public AsepriteFrameTag GetFrameTag(string name)
    {
        if (_frameTagDictionary.TryGetValue(name, out var tag))
        {
            return tag;
        }

        throw new KeyNotFoundException($"Frame tag '{name}' not found.");
    }
}