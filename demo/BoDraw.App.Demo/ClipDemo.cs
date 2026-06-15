using BoDraw;

public class ClipDemo
{
    public static void Draw(IBoDraw bd)
    {
        Polyline p = new Polyline();
        for (int i = 0; i < 2000; i++)
        {
            p.AddPoint(Random.Shared.NextDouble() * 100, Random.Shared.NextDouble() * 100);
        }

        AreaLikeShape c = new Circle(50, 50, 30);
        Clip clip = new Clip(p, c);

        c.LineColor = Colors.Red;
        c.LineThickness = 10;
        c.FillColor = null;

        bd.Add(clip, c);
    }
}
