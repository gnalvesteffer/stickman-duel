using System.Numerics;
using Raylib_cs;
using Xorberax.Duel.Core;
using Xorberax.Duel.Core.Aseprite;
using Xorberax.Duel.Core.Aseprite.Models;

namespace Xorberax.Duel.DuelGame;

public class DuelGame : Game
{
    private static readonly StickmanAnimationType[] AttackAnimations =
    [
        StickmanAnimationType.AttackHigh,
        StickmanAnimationType.AttackMiddle,
        StickmanAnimationType.AttackLow
    ];

    protected override string Title => "Stickman Duel by Xorberax";

    private readonly AsepriteAnimationInfo _stickmanAnimationInfo;

    private DuelGameState _state = new();
    private Texture2D _stickmanSpritesheetTexture;
    private Sound _hitSound;
    private Sound _hitDamageSound;

    public DuelGame(IAsepriteAnimationManager asepriteAnimationManager)
    {
        _stickmanAnimationInfo = asepriteAnimationManager.GetAnimationInfo(AssetPaths.StickmanAnimationJson);
    }

    protected override void Init()
    {
        _stickmanSpritesheetTexture = Raylib.LoadTexture(AssetPaths.StickmanSpritesheet);
        _hitSound = Raylib.LoadSound(AssetPaths.HitSound);
        _hitDamageSound = Raylib.LoadSound(AssetPaths.HitDamageSound);
        Reset();
    }

    protected override void Reset()
    {
        _state = new DuelGameState
        {
            PlayerStickman =
            {
                Position = new Vector2(Raylib.GetScreenWidth() * 0.5f + 55, Raylib.GetScreenHeight() * 0.5f),
                Animation = StickmanAnimationType.BlockHigh
            },
            OpponentStickman =
            {
                Position = new Vector2(Raylib.GetScreenWidth() * 0.5f - 55, Raylib.GetScreenHeight() * 0.5f),
                Animation = StickmanAnimationType.AttackHigh
            }
        };
    }

    protected override void Update(float deltaTime)
    {
        var opponentAnimationProgress = GetStickmanAnimationFrame(_state.OpponentStickman).AnimationProgress;
        if (opponentAnimationProgress > 1.0f)
        {
            if (_state.DidHandleAttack)
            {
                --_state.OpponentStickman.Health;
                Raylib.PlaySound(_hitSound);
                if (_state.OpponentStickman.Health <= 0)
                {
                    Reset();
                    return;
                }
            }
            else
            {
                --_state.PlayerStickman.Health;
                Raylib.PlaySound(_hitDamageSound);
                if (_state.PlayerStickman.Health <= 0)
                {
                    Reset();
                    return;
                }
            }

            var newOpponentAttack = AttackAnimations[Random.Shared.Next(AttackAnimations.Length)];
            _state.OpponentStickman.Animation = newOpponentAttack;
            _state.DidHandleAttack = false;
        }

        _state.PlayerStickman.Animation =
            Raylib.IsKeyDown(KeyboardKey.Up) ? StickmanAnimationType.BlockHigh :
            Raylib.IsKeyDown(KeyboardKey.Left) ? StickmanAnimationType.BlockMiddle :
            Raylib.IsKeyDown(KeyboardKey.Right) ? StickmanAnimationType.BlockMiddle :
            Raylib.IsKeyDown(KeyboardKey.Down) ? StickmanAnimationType.BlockLow :
            StickmanAnimationType.Idle;

        var opponentAnimation = _state.OpponentStickman.Animation;
        var playerAnimation = _state.PlayerStickman.Animation;
        _state.DidHandleAttack |= opponentAnimation is StickmanAnimationType.AttackHigh && playerAnimation is StickmanAnimationType.BlockHigh ||
                                  opponentAnimation is StickmanAnimationType.AttackMiddle && playerAnimation is StickmanAnimationType.BlockMiddle ||
                                  opponentAnimation is StickmanAnimationType.AttackLow && playerAnimation is StickmanAnimationType.BlockLow;
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
        DrawStickmanHealth(_state.PlayerStickman);
        DrawStickmanHealth(_state.OpponentStickman);
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
        var currentAnimationFrame = GetStickmanAnimationFrame(stickman);
        var currentFrameInfo = currentAnimationFrame.FrameInfo;

        Raylib.DrawEllipse(
            (int)stickman.Position.X,
            (int)(stickman.Position.Y + currentFrameInfo.Frame.H * 0.4f),
            (int)(currentFrameInfo.Frame.H * 0.5f),
            16,
            new Color(0.25f, 0.28f, 0.28f)
        );
    }

    private void DrawStickman(Stickman stickman, bool shouldFlipHorizontally, Color color)
    {
        var currentAnimationFrame = GetStickmanAnimationFrame(stickman);
        var currentFrameInfo = currentAnimationFrame.FrameInfo;

        Raylib.DrawTextureRec(
            _stickmanSpritesheetTexture,
            new Rectangle(
                currentFrameInfo.Frame.X + (shouldFlipHorizontally ? currentFrameInfo.Frame.W : 0),
                currentFrameInfo.Frame.Y,
                currentFrameInfo.Frame.W * (shouldFlipHorizontally ? -1 : 1),
                currentFrameInfo.Frame.H
            ),
            stickman.Position - new Vector2(
                currentFrameInfo.Frame.W * 0.5f,
                currentFrameInfo.Frame.H * 0.5f
            ),
            color
        );
    }

    private void DrawStickmanHealth(Stickman stickman)
    {
        const int healthBarWidth = 100;
        const int healthBarHeight = 10;
        const int healthBarOffsetY = -200;

        // No Health.
        Raylib.DrawRectangle(
            (int)(stickman.Position.X - healthBarWidth * 0.5f),
            (int)stickman.Position.Y + healthBarOffsetY,
            healthBarWidth,
            healthBarHeight,
            Color.Red
        );

        // Health.
        Raylib.DrawRectangle(
            (int)(stickman.Position.X - healthBarWidth * 0.5f),
            (int)stickman.Position.Y + healthBarOffsetY,
            healthBarWidth * stickman.Health / stickman.MaxHealth,
            healthBarHeight,
            Color.Green
        );
    }

    private (AsepriteFrameInfo FrameInfo, float AnimationProgress) GetStickmanAnimationFrame(Stickman stickman)
    {
        var frameTagName = stickman.Animation.ToAsepriteFrameTagName();
        var frameTag = _stickmanAnimationInfo.Meta.GetFrameTag(frameTagName);
        var animationStartFrame = frameTag.From;
        var animationEndFrame = frameTag.To;
        var totalAnimationFrames = animationEndFrame - animationStartFrame;
        var elapsedAnimationTime = Raylib.GetTime() - stickman.AnimationStartTime;
        var animationFrameRate = _stickmanAnimationInfo.FrameRate;
        var animationUnboundedFrameIndex = (float)(elapsedAnimationTime * animationFrameRate);
        var currentBoundedFrameIndex = (int)(animationStartFrame + animationUnboundedFrameIndex % totalAnimationFrames);
        var currentFrameInfo = _stickmanAnimationInfo.Frames[currentBoundedFrameIndex];
        var animationProgress = animationUnboundedFrameIndex / totalAnimationFrames;
        return (currentFrameInfo, animationProgress);
    }
}