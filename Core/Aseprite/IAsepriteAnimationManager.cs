using Xorberax.Duel.Core.Aseprite.Models;

namespace Xorberax.Duel.Core.Aseprite;

public interface IAsepriteAnimationManager
{
    /// <summary>
    ///     Gets the animation info from the given sprite sheet JSON file.
    /// </summary>
    /// <param name="spriteSheetJsonPath">Path relative to Assets folder.</param>
    AsepriteAnimationInfo GetAnimationInfo(string spriteSheetJsonPath);
}