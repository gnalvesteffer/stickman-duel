using System.Numerics;
using Raylib_cs;
using Xorberax.Duel.Core;
using Xorberax.Duel.Core.Aseprite;
using Xorberax.Duel.Core.Aseprite.Models;

namespace Xorberax.Duel.DuelGame;

public class DuelGame : Game
{
    protected override string Title => "Stickman Duel by Xorberax";

    private readonly AsepriteAnimationInfo _stickmanAnimationInfo;

    private DuelGameState _state = new();
    private Texture2D _stickmanSpritesheetTexture;

    public DuelGame(IAsepriteAnimationManager asepriteAnimationManager)
    {
        _stickmanAnimationInfo = asepriteAnimationManager.GetAnimationInfo(AssetPaths.StickmanAnimationJson);
    }

    protected override void Init()
    {
        _stickmanSpritesheetTexture = Raylib.LoadTexture(AssetPaths.StickmanSpritesheet);
        Reset();
    }

    protected override void Reset()
    {
        _state = new DuelGameState();
        _state.PlayerStickman = _state.PlayerStickman with { Position = new Vector2(Raylib.GetScreenWidth() * 0.5f + 16, Raylib.GetScreenHeight() * 0.5f) };
        _state.OpponentStickman = _state.OpponentStickman with { Position = new Vector2(Raylib.GetScreenWidth() * 0.5f - 16, Raylib.GetScreenHeight() * 0.5f) };
    }

    protected override void Update(float deltaTime)
    {
    }

    protected override void Draw()
    {
        Raylib.ClearBackground(Color.Gray);
        Raylib.DrawText(Title, 10, 10, 20, Color.Black);

        DrawStickman(_state.PlayerStickman, true, Color.DarkBlue);
        DrawStickman(_state.OpponentStickman, false, Color.Red);
    }

    private void DrawStickman(Stickman stickman, bool shouldFlipHorizontally, Color color)
    {
        var frameTagName = stickman.AnimationType.ToAsepriteFrameTagName();
        var frameTag = _stickmanAnimationInfo.Meta.GetFrameTag(frameTagName);
        var animationStartFrame = frameTag.From;
        var animationEndFrame = frameTag.To;
        var totalAnimationFrames = animationEndFrame - animationStartFrame;
        var elapsedAnimationTime = Raylib.GetTime() - stickman.AnimationStartTime;
        var animationFrameRate = _stickmanAnimationInfo.FrameRate;
        var currentFrameIndex = (int)(elapsedAnimationTime * animationFrameRate) % totalAnimationFrames;
        var currentFrameInfo = _stickmanAnimationInfo.Frames[currentFrameIndex];

        Raylib.DrawTextureRec(
            _stickmanSpritesheetTexture,
            new Rectangle(
                currentFrameInfo.Frame.X + (shouldFlipHorizontally ? currentFrameInfo.Frame.W : 0),
                currentFrameInfo.Frame.Y,
                currentFrameInfo.Frame.W * (shouldFlipHorizontally ? -1 : 1),
                currentFrameInfo.Frame.H
            ),
            stickman.Position -
            new Vector2(
                currentFrameInfo.Frame.W * 0.5f,
                currentFrameInfo.Frame.H * 0.5f
            ),
            color
        );
    }
}