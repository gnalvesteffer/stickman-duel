using Raylib_cs;

namespace Xorberax.Duel.Core;

public abstract class Game
{
    protected virtual string Title => "Game";
    
    public void Start()
    {
        Raylib.InitWindow(1280, 700, Title);

        Init();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.PollInputEvents();
            Tick(Raylib.GetFrameTime());
        }

        Raylib.CloseWindow();
    }

    protected abstract void Init();
    protected abstract void Reset();
    protected abstract void Update(float deltaTime);
    protected abstract void Draw();

    protected void Tick(float deltaTime)
    {
        Update(deltaTime);
        InternalDraw();
    }

    private void InternalDraw()
    {
        Raylib.BeginDrawing();
        Draw();
        Raylib.EndDrawing();
    }
}