using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    int width = 800;
    int height = 600;
    //balloon varables
    int balloonOffset = 30;
    const int balloonCount = 25;

    float[] balloonX = new float[balloonCount];
    float[] balloonY = new float[balloonCount];
    float[] balloonSpeed = new float[balloonCount];
    bool[] balloonAlive = new bool[balloonCount];
    Color[] balloonColors = new Color[balloonCount];
    
    int health = 3;

    System.Random random = new System.Random();
    

    public void Setup()
    {
        Window.SetTitle("Pop the Balloons");
        Window.SetSize(width, height);
        Draw.LineColor = Color.Clear;

        for (int i = 0; i < balloonCount; i++)
        {
            //giving balloon varables value
            balloonX[i] = random.Next(balloonOffset, width - balloonOffset);
            balloonY[i] = random.Next(balloonOffset, height - balloonOffset);
            balloonSpeed[i] = (float)(random.NextDouble() * 1.5 + 0.5f);
            balloonAlive[i] = true;

            balloonColors[i] = new Color(
                random.Next(150, 255),
                random.Next(100, 255),
                random.Next(100, 255)
            );
        }
    }

    public void Update()
    {
        Window.ClearBackground(Color.OffWhite);

        Vector2 mousePos = Input.GetMousePosition();
        bool isMousePressed = Input.IsMouseButtonPressed(0);

        bool allPopped = true;

        for (int i = 0; i < balloonCount; i++)
        {
            if (!balloonAlive[i])
                continue;

            allPopped = false;

            if (isMousePressed)
            {
                float dx = mousePos.X - balloonX[i];
                float dy = mousePos.Y - balloonY[i];
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                if (distance <= 20f)
                {
                    balloonAlive[i] = false;
                    continue;
                }
            }

            // Draw balloon
            Draw.LineColor = Color.Gray;
            Draw.LineSize = 2;
            Draw.Line(balloonX[i], balloonY[i] + 20, balloonX[i], balloonY[i] + 60);

            Draw.FillColor = balloonColors[i];
            Draw.Circle(balloonX[i], balloonY[i], 20);

            balloonY[i] -= balloonSpeed[i];

            if (balloonY[i] < -20)
            {
                balloonY[i] = height - balloonOffset;
                balloonX[i] = random.Next(balloonOffset, width - balloonOffset);
                if (balloonAlive[i])
                {
                    health--;
                }
            }
        }

        // Draw health
        for (int i = 0; i < 3; i++)
        {
            if (i < health)
                Draw.FillColor = new Color(200, 0, 0);
            else
                Draw.FillColor = new Color(100, 100, 100);

            Draw.LineSize = 0;
            Draw.Rectangle(10 + i * 40, 10, 30, 30);
        }

        // Lose Screen
        if (health <= 0)
        {
            int startRed = 125;
            int endRed = 255;

            for (int x = 0; x < width; x += 5)
            {
                int redValue = startRed + (endRed - startRed) * x / width;
                redValue = Math.Min(redValue, 255);

                Draw.FillColor = new Color(redValue, 0, 0);
                Draw.LineSize = 0;
                Draw.Rectangle(x, 0, 5, height);
            }
            return;
        }

        // Win Screen
        if (allPopped)
        {
            int startGreen = 125;
            int endGreen = 255;

            for (int x = 0; x < width; x += 5)
            {
                int greenValue = startGreen + (endGreen - startGreen) * x / width;
                greenValue = Math.Min(greenValue, 255);

                Draw.FillColor = new Color(0, greenValue, 0);
                Draw.LineSize = 0;
                Draw.Rectangle(x, 0, 5, height);
            }
        }
    }
}