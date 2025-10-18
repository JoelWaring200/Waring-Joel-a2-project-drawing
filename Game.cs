using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    int width = 800;
    int height = 600;

    int balloonOffSet = 30;

    const int balloonCount = 25;

    float[] balloonX = new float[balloonCount];
    float[] balloonY = new float[balloonCount];
    bool[] balloonAlive = new bool[balloonCount];
    Color[] balloonColors = new Color[balloonCount];

    System.Random random = new System.Random();

    int health = 3;
    public void Setup()
    {
        

        Window.SetTitle("Pop the Balloons");
        Window.SetSize(width, height);
        Draw.LineColor = Color.Clear;

        

        for (int i = 0; i < balloonCount; i++)
        {
            balloonX[i] = random.Next(balloonOffSet, width - balloonOffSet);
            balloonY[i] = random.Next(balloonOffSet, height - balloonOffSet);
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
        bool isMouseDown = Input.IsMouseButtonDown(0);

        bool allPopped = true;

        for (int i = 0; i < balloonCount; i++)
        {
            if (!balloonAlive[i])
            {
                continue;
            }
                
            allPopped = false;

            if (isMouseDown)
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

            Draw.LineColor = Color.Gray;
            Draw.Line(balloonX[i], balloonY[i] + 20, balloonX[i], balloonY[i] + 60);

            Draw.FillColor = balloonColors[i];
            Draw.Circle(balloonX[i], balloonY[i], 20);

            balloonY[i] -= 0.5f;

            if (balloonY[i] < -20)
            {
                balloonY[i] = height - balloonOffSet;
                balloonX[i] = random.Next(30, width - balloonOffSet);
                if (balloonAlive[i])
                {
                    health--;
                }
            }
        }

        if (health <= 0)
        {
            int startRed = 125;
            int endRed = 255;

            for (int x = 0; x < width; x += 5)
            {
                int redValue = startRed + (endRed - startRed) * x / width;
                if (redValue > 255) redValue = 255;

                Draw.FillColor = new Color(redValue, 0, 0);
                Draw.LineSize = 0;
                Draw.Rectangle(x, 0, 5, height);
            }
        }

        if (allPopped)
        {
            int startGreen = 125;
            int endGreen = 255;

            for (int x = 0; x < width; x += 5)
            {
                int greenValue = startGreen + (endGreen - startGreen) * x / width;
                if (greenValue > 255) greenValue = 255;

                Draw.FillColor = new Color(0, greenValue, 0);
                Draw.LineSize = 0;
                Draw.Rectangle(x, 0, 5, height);
            }
        }
    }


}
