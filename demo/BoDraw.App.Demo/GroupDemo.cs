using BoDraw;

public class GroupDemo
{
    public static void Draw(IBoDraw bd)
    {
        Group g1 = new Group(
            new Rectangle(0, 0, 12, 8).WithFillColor(Colors.HotPink),
            new Rectangle(0, 4, 12, 12).WithFillColor(Colors.Orange).WithFillOpacity(0.7),
            new Circle(12, 6, 6).WithFillColor(Colors.FromRgb(20, 255, 140)).WithFillOpacity(0.3)
        );
        Group g2 = g1.Copy(20, 0);
        Shape g3 = g1.Copy().FitInto(new Rectangle(40, -1, 50, 13));
        bd.Add(g1, g2, g3);
    }
}