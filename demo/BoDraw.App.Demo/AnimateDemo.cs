using BoDraw;
using static System.Math;

public class AnimateDemo
{
    public static void Draw(IBoDraw bd)
    {
        bd.Animate(2 * PI, t =>
        {
            bd.Clear();
            bd.Add(
                new Circle(0, 0, 50),
                new Circle(45 * Cos(t), 45 * Sin(t), 5).WithFillColor(Colors.HotPink)
            );
        });

    }
}
