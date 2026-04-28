using BoDraw;

internal class Demo4
{
    internal static void Draw(BoDrawApp bd)
    {
        Rectangle r1 = new Rectangle(0, 0, 12, 8);
        r1.FillColor = Colors.HotPink;

        Rectangle r2 = new Rectangle(0, 4, 12, 12);
        r2.FillColor = Colors.Orange;
        r2.FillOpacity = 0.7;

        Circle c = new Circle(12, 6, 6);
        c.FillColor = Colors.FromRgb(20, 255, 140);
        c.FillOpacity = 0.3;

        Group g = new Group(r1, r2, c);
        g.Move(18, 0);

        bd.Add(r1, r2, c, g);
    }
}