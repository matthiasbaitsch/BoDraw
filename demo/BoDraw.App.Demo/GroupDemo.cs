using BoDraw;

public class GroupDemo
{
    public static void Draw(IBoDraw bd)
    {
        Text t1 = new Text("Top left", 0, 10, 2).WithJust(0, 0);
        Text t2 = new Text("Center", 6, 6, 2).WithJust(0.5, 0.5);
        Text t3 = new Text("Bottom right", 12, 2, 2).WithJust(1, 1);

        Group g1 = new Group(
            new Rectangle(0, 0, 12, 8).WithFillColor(Colors.HotPink),
            new Rectangle(0, 4, 12, 12).WithFillColor(Colors.Orange).WithFillOpacity(0.7),
            new Circle(12, 6, 6).WithFillColor(Colors.FromRgb(20, 255, 140)).WithFillOpacity(0.3),
            t1, t2, t3
        );
        Shape g2 = g1.Copy(15, 0).Scale(0.5);
        Shape g3 = g1.Copy().FitInto(new Rectangle(30, -1, 40, 13));
        bd.Add(g1, g2, g3);
    }
}