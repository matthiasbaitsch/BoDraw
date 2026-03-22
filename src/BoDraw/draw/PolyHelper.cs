using Avalonia;
using Avalonia.Media;

namespace BoDraw;

internal class PolyHelper
{
    private readonly List<Point> points = [];

    internal Rect Bounds
    {
        get
        {
            if (this.points.Count == 0)
            {
                return new Rect(0, 0, 0, 0);
            }

            Point p = this.points[0];
            double xmin = p.X;
            double xmax = p.X;
            double ymin = p.Y;
            double ymax = p.Y;

            for (int i = 1; i < this.points.Count; i++)
            {
                p = this.points[i];
                xmin = Math.Min(xmin, p.X);
                xmax = Math.Max(xmax, p.X);
                ymin = Math.Min(ymin, p.Y);
                ymax = Math.Max(ymax, p.Y);
            }

            return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
        }
    }

    internal void AddPoint(double x, double y)
    {
        this.points.Add(new Point(x, y));
    }

    internal void Draw(DrawingContext ctx, bool isPolygon, Brush? brush, Pen? pen)
    {
        // Quick return
        if (this.points.Count < 2) { return; }

        // Construct geometry
        var geo = new StreamGeometry();
        using (var sgc = geo.Open())
        {
            sgc.BeginFigure(this.points[0], isPolygon);
            for (int i = 1; i < this.points.Count; i++)
            {
                sgc.LineTo(this.points[i]);
            }
            sgc.EndFigure(isPolygon);
        }

        // Draw geometry
        ctx.DrawGeometry(brush, pen, geo);
    }
}