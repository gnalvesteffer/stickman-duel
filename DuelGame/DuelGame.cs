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

        _state.PlayerStickman.Position = new Vector2(Raylib.GetScreenWidth() * 0.5f + 65, Raylib.GetScreenHeight() * 0.5f);
        _state.OpponentStickman.Position = new Vector2(Raylib.GetScreenWidth() * 0.5f - 65, Raylib.GetScreenHeight() * 0.5f);

        _state.PlayerStickman.AnimationType = StickmanAnimationType.BlockHigh;
        _state.OpponentStickman.AnimationType = StickmanAnimationType.AttackHigh;
    }

    protected override void Update(float deltaTime)
    {
    }

    protected override void Draw()
    {
        Raylib.ClearBackground(Color.Gray);
        Raylib.DrawText(Title, 10, 10, 20, Color.Black);

        DrawBackground();
        DrawStickmanShadow(_state.PlayerStickman);
        DrawStickmanShadow(_state.OpponentStickman);
        DrawStickman(_state.PlayerStickman, true, Color.DarkBlue);
        DrawStickman(_state.OpponentStickman, false, Color.Red);
    }

    private void DrawBackground()
    {
        Raylib.DrawRectangle(
            0,
            (int)(Raylib.GetScreenHeight() * 0.5),
            Raylib.GetScreenWidth(),
            (int)(Raylib.GetScreenHeight() * 0.5),
            Color.DarkGray
        );
    }

    private void DrawStickmanShadow(Stickman stickman)
    {
        var currentFrameInfo = GetStickmanCurrentFrameInfo(stickman);

        Raylib.DrawEllipse(
            (int)stickman.Position.X,
            (int)(stickman.Position.Y + currentFrameInfo.Frame.H * 0.4f),
            (int)(currentFrameInfo.Frame.H * 0.5f),
            16,
            Color.Black
        );
    }

    private void DrawStickman(Stickman stickman, bool shouldFlipHorizontally, Color color)
    {
        var currentFrameInfo = GetStickmanCurrentFrameInfo(stickman);

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

    private AsepriteFrameInfo GetStickmanCurrentFrameInfo(Stickman stickman)
    {
        var frameTagName = stickman.AnimationType.ToAsepriteFrameTagName();
        var frameTag = _stickmanAnimationInfo.Meta.GetFrameTag(frameTagName);
        var animationStartFrame = frameTag.From;
        var animationEndFrame = frameTag.To;
        var totalAnimationFrames = animationEndFrame - animationStartFrame;
        var elapsedAnimationTime = Raylib.GetTime() - stickman.AnimationStartTime;
        var animationFrameRate = _stickmanAnimationInfo.FrameRate;
        var animationProgress = elapsedAnimationTime * animationFrameRate;
        var currentFrameIndex = (int)(animationStartFrame + animationProgress % totalAnimationFrames);
        var currentFrameInfo = _stickmanAnimationInfo.Frames[currentFrameIndex];
        return currentFrameInfo;
    }
}