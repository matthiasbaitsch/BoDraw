using BoDraw;

public class PolygonDemo
{
    public static void Draw(IBoDraw bd)
    {
        Rectangle rectangle = new Rectangle(-20, -20, 420, 220);

        // Construct with some points
        Polygon s1 = new Polygon(0, 55, 7, 17, 27, 27, 14, 6, 50, 0, 18, -9);

        // Add some more points
        s1.AddPoint(27, -32);
        s1.AddPoint(4, -11);
        s1.AddPoint(-5, -52);
        s1.AddPoint(-7, -15);
        s1.AddPoint(-25, -25);
        s1.AddPoint(-14, -6);
        s1.AddPoint(-48, 0);
        s1.AddPoint(-17, 7);
        s1.AddPoint(-28, 28);
        s1.AddPoint(-6, 15);

        // Configure
        s1.Scale(0.25);
        s1.Move(-20, -20);
        s1.LineThickness = 2;
        s1.LineColor = Colors.Red;
        s1.FillColor = Colors.Yellow;

        // Add
        bd.Add(rectangle, s1, s1.Copy(440, 0), s1.Copy(0, 240), s1.Copy(440, 240));
    }
}