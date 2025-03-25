using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

class Game
{
    public static readonly int screenWidth = 1920;
    public static readonly int screenHeight = 1080;

    enum GameState{ Edit, Play }
    private static GameState gameState = GameState.Edit;

    private static CellularAutomata cellularAutomata = new(screenWidth/5, screenHeight/5, 5);

    private static double timer = 0;
    private static double timerDuration = 0;

    private static void UpdateEditMode()
    {
        Vector2 mousePosition = GetMousePosition();
        (int column, int row) = cellularAutomata.ToGrid(mousePosition);
        if (IsMouseButtonPressed(MouseButton.Left)) cellularAutomata.SetCell(column, row, true);
        if (IsMouseButtonPressed(MouseButton.Right)) cellularAutomata.SetCell(column, row, false);
    }

    private static void UpdateSimulation()
    {
        timer += GetFrameTime();
        if (timer >= timerDuration)
        {
            timer = 0;
            cellularAutomata.Update();
        }
    }

    private static void Update()
    {
        switch(gameState)
        {
            case GameState.Edit:
                UpdateEditMode();
                break;
            case GameState.Play:
                UpdateSimulation();
                break;
        }

        if (IsKeyPressed(KeyboardKey.Space)) gameState = gameState == GameState.Edit ? GameState.Play : GameState.Edit;
        if (IsKeyPressed(KeyboardKey.R)) cellularAutomata.Randomize();

        if (IsKeyPressed(KeyboardKey.Up)) timerDuration -= 0.01;
        if (IsKeyPressed(KeyboardKey.Down)) timerDuration += 0.01;
        timerDuration = Math.Clamp(timerDuration, 0.01, 1);
    }

    private static void Draw()
    {
        for (int column = 0; column < cellularAutomata.columns; column++)
        {
            for (int row = 0; row < cellularAutomata.rows; row++)
            {
                Vector2 cellPosition = cellularAutomata.ToWorld(column, row);
                Rectangle cellRectangle = new Rectangle(cellPosition.X, cellPosition.Y, cellularAutomata.cellSize, cellularAutomata.cellSize);
                if (cellularAutomata.GetCell(column, row)) DrawRectangleRec(cellRectangle, Color.White);
            }
        }
    }

    static void Main()
    {
        InitWindow(screenWidth, screenHeight, "Game");
        SetTargetFPS(60);
        while (!WindowShouldClose())
        {
            Update();
            BeginDrawing();
            ClearBackground(Color.Black);
            Draw();
            EndDrawing();
        }
        CloseWindow();
    }
}
