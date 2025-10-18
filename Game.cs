using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    const int balloonCount = 25;

    float[] balloonX = new float[balloonCount];
    float[] balloonY = new float[balloonCount];
    bool[] balloonAlive = new bool[balloonCount];
    Color[] balloonColors = new Color[balloonCount];

    System.Random random = new System.Random();
    public void Setup()
    {
        Window.SetTitle("Pop the Balloons");
        Window.SetSize(800, 600);
        Draw.LineColor = Color.Clear;

        for (int i = 0; i < balloonCount; i++)
        {
            balloonX[i] = random.Next(30, 770);
            balloonY[i] = random.Next(200, 600);
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

        for (int i = 0; i < balloonCount; i++)
        {
            if (!balloonAlive[i])
                continue;

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
                balloonY[i] = 420;
                balloonX[i] = random.Next(30, 370);
            }
        }
    }
}
