using BoDraw;

public class GLogoDemo
{
    public static void Draw(IBoDraw bd)
    {
        int n = 5;
        double h = 20;
        double r = 20;
        double d = 2.5;
        double w = (n - 1) * d / 2;

        PathShape path = new PathShape();

        path.FillColor = null;
        path.LineThickness = 10;
        for (int i = 0; i < n; i++)
        {
            path.MoveTo(r, h / 2);
            path.ArcTo(-r, h / 2, r);
            path.LineTo(-r, -h / 2);
            path.ArcTo(r, -h / 2, r);
            path.LineTo(r, w);
            path.LineTo(0, w);
            r -= d;
            w -= d;
        }

        bd.Add(path);

    }
}
