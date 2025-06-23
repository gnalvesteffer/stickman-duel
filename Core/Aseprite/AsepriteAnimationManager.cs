using Newtonsoft.Json;
using Xorberax.Duel.Core.Aseprite.Models;

namespace Xorberax.Duel.Core.Aseprite;

public class AsepriteAnimationManager : IAsepriteAnimationManager
{
    /// <summary>
    ///     Cache of animation info, keyed by sprite sheet JSON path.
    /// </summary>
    private readonly Dictionary<string, AsepriteAnimationInfo> _animationInfoCache = new();

    /// <summary>
    ///     Gets the animation info from the given sprite sheet JSON file.
    /// </summary>
    /// <param name="spriteSheetJsonPath">Path relative to Assets folder.</param>
    public AsepriteAnimationInfo GetAnimationInfo(string spriteSheetJsonPath)
    {
        var fileContents = File.ReadAllText(spriteSheetJsonPath);
        if (_animationInfoCache.TryGetValue(spriteSheetJsonPath, out var cachedInfo))
        {
            return cachedInfo;
        }
        
        var animationInfo = JsonConvert.DeserializeObject<AsepriteAnimationInfo>(fileContents);
        _animationInfoCache[spriteSheetJsonPath] = animationInfo;
        return animationInfo;
    }
}