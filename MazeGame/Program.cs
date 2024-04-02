// Created by Jonah, Makayla, Eamon, Sudhan and Param
using Raylib_cs;
using System;
using System.Diagnostics;
using System.Numerics;

namespace MazeGame
{

    internal class Program
    {
        // Setup window
        const string title = "Maze game created for Mohawk College";
        const int width = 800;
        const int height = 800;

        // Setup drawing
        public static int startY = 0;
        public static int startX = 400;
        public static int currentX = startX;
        public static int currentY = startY;
        public static int a;
        public static int numOfTilesToMove = 4;
        public static int currentTile = 0;

        // Ball variables
        const int ballRadius = 20;
        const float ballSpeed = 200f;
        public static Vector2 ballPosition = new Vector2(width / 2, height / 2);

        // Game completed check variables
        public static bool gameOneCompleted = false;
        public static bool gameTwoCompleted = false;
        public static bool gameThreeCompleted = false;

        static Stopwatch timer = new Stopwatch();
        static TimeSpan elapsedTime;
        static void Main(string[] args)
        {
            Raylib.InitWindow(width, height, title);
            Raylib.SetTargetFPS(60);
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DarkGreen);
                Update();
                //drawTree();
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }

        static void Update()
        {

            //Console.WriteLine("Running game"); // Debug 
            // Your game code run each frame here
            if (gameOneCompleted == false)
            {
                drawLevelOne();
                UpdateBall();
            }
            else if (gameOneCompleted && gameTwoCompleted == false)
            {
                drawLevelTwo();
                UpdateBall();
            }
            else if (gameOneCompleted && gameTwoCompleted && gameThreeCompleted == false)
            {
                drawLevelThree();
                UpdateBall();
            }
            if (gameOneCompleted && gameTwoCompleted && gameThreeCompleted)
            {
                timer.Stop();
            }
        }
        static void UpdateBall()
        {
            Vector2 ballDirection = new Vector2(0, 0);

            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                ballDirection = new Vector2(1, 0);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                ballDirection = new Vector2(-1, 0);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                ballDirection = new Vector2(0, -1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                ballDirection = new Vector2(0, 1);
            }

            ballPosition += ballDirection * ballSpeed * Raylib.GetFrameTime();

            if (ballPosition.X > width)
                ballPosition.X = 0;
            else if (ballPosition.X < 0)
                ballPosition.X = width;

            if (ballPosition.Y > height)
                ballPosition.Y = 0;
            else if (ballPosition.Y < 0)
                ballPosition.Y = height;

            Raylib.DrawCircle((int)ballPosition.X, (int)ballPosition.Y, ballRadius, Color.Yellow);
        }
        static void drawLevelOne()
        {
            currentX = startX;
            currentY = startY;
            goDown();
            drawStart();
            goDown();
            goDown();
            drawEnd();
        }
        static void drawLevelTwo()
        {
            currentX = startX;
            currentY = startY;
            goRight();
            drawStart();
            goDown();
            goLeft();
            goLeft();
            goUp();
            goLeft();
            goDown();
            goDown();
            goRight();
            goRight();
            goRight();
            goDown();
            goLeft();
            goLeft();
            goLeft();
            drawEnd();
        }
        static void drawLevelThree()
        {
            currentX = startX;
            currentY = startY;
            goRight();
            drawStart();
            goDown();
            goDown();
            goDown();
            goLeft();
            goLeft();
            goLeft();
            goUp();
            goUp();
            goUp();
            goRight();
            goDown();
            goDown();
            goRight();
            goUp();
            drawEnd();
        }
        static void drawStart()
        {
            Raylib.DrawRectangle(400, 0, 50, 50, Color.Green);
        }
        static void drawEnd()
        {
            Raylib.DrawRectangle(currentX, currentY, 50, 50, Color.Red);
        }
        static void goRight()
        {
            while (true) // Go right with path
            {

                currentX = currentX + a;
                currentTile++;
                if (currentTile > numOfTilesToMove)
                {
                    currentTile = 0;
                    a = 0;
                    break;

                }
                currentX += 50;
                Raylib.DrawRectangle(currentX, currentY, 50, 50, Color.Blue);
            }
            return;
        }
        static void goLeft()
        {
            while (true) // Go left with path
            {

                currentX = currentX + a;
                currentTile++;
                if (currentTile > numOfTilesToMove)
                {
                    currentTile = 0;
                    a = 0;
                    break;
                }
                currentX -= 50;
                Raylib.DrawRectangle(currentX, currentY, 50, 50, Color.Blue);
            }
            return;
        }
        static void goDown()
        {
            while (true) // Go down with path
            {

                currentY = currentY + a;
                currentTile++;
                if (currentTile > numOfTilesToMove)
                {
                    currentTile = 0;
                    a = 0;
                    break;
                }

                currentY += 50;
                Raylib.DrawRectangle(currentX, currentY, 50, 50, Color.Blue);
            }
            return;
        }
        static void goUp()
        {
            while (true) // Go down with path
            {

                currentY = currentY + a;
                currentTile++;
                if (currentTile > numOfTilesToMove)
                {
                    currentTile = 0;
                    a = 0;
                    break;
                }
                currentY -= 50;
                Raylib.DrawRectangle(currentX, currentY, 50, 50, Color.Blue);
            }
            return;
        }
        static void DrawTimer()
        {
            elapsedTime = timer.Elapsed;
            string timerText = $"Time: {elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}.{elapsedTime.Milliseconds / 10:00}";
            Raylib.DrawText(timerText, 10, 10, 20, Color.White);
        }
    }
}