using BoDraw;

public class PolylineDemo
{
    public static void Draw(IBoDraw bd)
    {
        // AddPoint
        var p1 = new Polyline();
        p1.AddPoint(0, 0);
        p1.AddPoint(3, 4);
        p1.AddPoint(6, 1);
        p1.AddPoint(9, 5);

        // Flat coordinate list
        var p2 = new Polyline(10, 5, 13, 1, 16, 5, 19, 0).WithColor(Colors.Green);

        // Data sequences mapped into a rectangle
        var xs = Enumerable.Range(0, 500).Select(i => 19.0 * i / 500);
        var ys = xs.Select(x => Math.Sin(x) - 2);
        var p3 = new Polyline(xs, ys).WithColor(Colors.Red);

        bd.Add(p1, p2, p3);
    }
}
