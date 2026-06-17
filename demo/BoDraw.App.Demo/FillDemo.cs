using BoDraw;

public class FillDemo
{
    public static void Draw(IBoDraw bd)
    {
        bd.Add(
            new Rectangle(0, 0, 12, 8).WithFillColor(Colors.HotPink),
            new Rectangle(0, 4, 12, 12).WithFillColor(Colors.Orange).WithFillOpacity(0.7),
            new Circle(12, 6, 6).WithFillColor(Colors.FromRgb(20, 255, 140)).WithFillOpacity(0.3)
        );
    }
}
