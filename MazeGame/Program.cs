
﻿// Created by Jonah, Makayla, Eamon, Sudhan and Param
using Raylib_cs;
using System;
using System.Diagnostics;
using System.Numerics;

namespace ConsoleApp1
{
    internal class Program
    {
        // Setup window
        const string title = "Maze game created for Mohawk College";
        const int width = 800;
        const int height = 800;

        // Ball variables
        const int ballRadius = 20;
        const float ballSpeed = 200f;
        const int speedsensitivty = 1;
        public static Vector2 ballPosition = new Vector2(width / 2, height / 2);

        // Setup drawing
        public static int startX = 400;
        public static int startY = 0;
        public static int currentX = startX;
        public static int currentY = startY;
        public static int a;
        public static int numOfTilesToMove = 4;
        public static int currentTile = 0;

        public static int squareWidth = 50;
        public static int squareHeight = 50;

        // Game completed check variables
        public static bool gameOneCompleted = false;
        public static bool gameTwoCompleted = false;
        public static bool gameThreeCompleted = false;

        // Timer variabbles
        static Stopwatch timer = new Stopwatch();
        static TimeSpan elapsedTime;
        static void Main(string[] args)
        {
            Raylib.InitWindow(width, height, title);
            Raylib.SetTargetFPS(60);

            // Display "press Enter to start" screen
            DrawStartScreen();

            // Wait for Enter key press
            while (!Raylib.IsKeyDown(KeyboardKey.Enter))
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                DrawStartScreen();
                Raylib.EndDrawing();
            }

            // Start the game loop
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DarkGreen);
                Update();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        static void DrawStartScreen()
        {
            // Your game code run each frame here
            Raylib.DrawText("Welcome to the Maze Game!", width / 2 - 150, height / 2 - 50, 20, Color.White);
            Raylib.DrawText("Use WASD keys to move the ball through the maze.", width / 2 - 220, height / 2 - 20, 20, Color.White);
            Raylib.DrawText("          Reach the red square to complete each level.", width / 2 - 290, height / 2 + 10, 20, Color.White);
            Raylib.DrawText("Press Enter to start", width / 2 - 100, height / 2 + 40, 20, Color.White);
        }

        static void Update()
        {
            // Update game state
            if (gameOneCompleted == false)
            {
                drawLevelOne(); // Draw level one
                UpdateBall(); // Update ball movement
            }
            else if (gameTwoCompleted == false)
            {
                drawLevelTwo(); // Draw level two
                UpdateBall(); // Update ball movement
            }
            else if (gameThreeCompleted == false)
            {
                drawLevelThree(); // Draw level three
                UpdateBall(); // Update ball movement
            }

            // Check if all levels are completed
            if (gameOneCompleted && gameTwoCompleted && gameThreeCompleted)
            {
                // Optionally add code here for any actions upon completing all levels
            }
        }

        static void UpdateBall()
        {
            Vector2 ballDirection = new Vector2(0, 0);

            // Check keyboard input for ball movement
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                ballDirection = new Vector2(speedsensitivty, 0); // Move right
            }
            else if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                ballDirection = new Vector2(-speedsensitivty, 0); // Move left
            }
            else if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                ballDirection = new Vector2(0, -speedsensitivty); // Move up
            }
            else if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                ballDirection = new Vector2(0, speedsensitivty); // Move down
            }

            // Update ball position based on direction and speed
            ballPosition += ballDirection * ballSpeed * Raylib.GetFrameTime();

            // Wrap around the screen if ball reaches the edge
            if (ballPosition.X > width)
                ballPosition.X = 0;
            else if (ballPosition.X < 0)
                ballPosition.X = width;

            if (ballPosition.Y > height)
                ballPosition.Y = 0;
            else if (ballPosition.Y < 0)
                ballPosition.Y = height;

            // Draw the ball
            Raylib.DrawCircle((int)ballPosition.X, (int)ballPosition.Y, ballRadius, Color.Yellow);

            // Check for level completion
            if (Raylib.CheckCollisionCircleRec(ballPosition, ballRadius, new Rectangle(currentX, currentY, 50, 50)))
            {
                // Check which level is completed and set the corresponding flag
                if (!gameOneCompleted)
                {
                    gameOneCompleted = true;
                    ResetBall(); // Reset ball position
                    return;
                }
                else if (!gameTwoCompleted)
                {
                    gameTwoCompleted = true;
                    ResetBall(); // Reset ball position
                    return;
                }
                else if (!gameThreeCompleted)
                {
                    gameThreeCompleted = true;
                    ResetBall(); // Reset ball position
                    return;
                }
            }
        }

        static void ResetBall()
        {
            ballPosition = new Vector2(startX + 25, startY + 25); // Reset ball position to start
        }

        // Draw Level 1
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

        // Draw Level 2
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

        // Draw Level 3
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

        // Draw start point
        static void drawStart()
        {
            // Draws starting square
            Raylib.DrawRectangle(400, 0, squareWidth, 50, Color.Green);
        }

        // Draw end point
        static void drawEnd()
        {
            // Draws ending square
            Raylib.DrawRectangle(currentX, currentY, squareWidth, 50, Color.Red);
        }

        // Functions for moving the cursor
        static void goRight()
        {
            for (int i = 0; i < numOfTilesToMove; i++)
            {
                currentX += 50; // Move right
                Raylib.DrawRectangle(currentX, currentY, squareWidth, squareHeight, Color.Gray); // Draw tile
            }
        }

        static void goLeft()
        {
            for (int i = 0; i < numOfTilesToMove; i++)
            {
                currentX -= 50; // Move left
                Raylib.DrawRectangle(currentX, currentY, squareWidth, squareHeight, Color.Gray); // Draw tile
            }
        }

        static void goDown()
        {
            for (int i = 0; i < numOfTilesToMove; i++)
            {
                currentY += 50; // Move down
                Raylib.DrawRectangle(currentX, currentY, squareWidth, squareHeight, Color.Gray); // Draw tile
            }
        }

        static void goUp()
        {
            for (int i = 0; i < numOfTilesToMove; i++)
            {
                currentY -= 50; //Move up
                Raylib.DrawRectangle(currentX, currentY, squareWidth, squareHeight, Color.Gray); // Draw title
            }
        }
        static void DrawTimer()
        {
            elapsedTime = timer.Elapsed;
            string timerText = $"Time: {elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}.{elapsedTime.Milliseconds / 10:00}";
            Raylib.DrawText(timerText, 10, 10, 20, Color.White);
        }
    }
}
