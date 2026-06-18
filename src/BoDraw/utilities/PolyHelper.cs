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

    internal void AddPoints(double[] coordinates)
    {
        for (int i = 0; i < coordinates.Length - 1; i += 2)
        {
            this.AddPoint(coordinates[i], coordinates[i + 1]);
        }
    }

    internal void AddPoints(IEnumerable<double> xs, IEnumerable<double> ys)
    {
        foreach (var (x, y) in xs.Zip(ys))
        {
            this.AddPoint(x, y);
        }
    }

    internal void Scale(double sx, double sy)
    {
        double cx = this.Bounds.X + this.Bounds.Width / 2;
        double cy = this.Bounds.Y + this.Bounds.Height / 2;
        for (int i = 0; i < this.points.Count; i++)
        {
            this.points[i] = new Point(
                cx + (this.points[i].X - cx) * sx,
                cy + (this.points[i].Y - cy) * sy
            );
        }
    }

    internal void Scale(double factor)
    {
        this.Scale(factor, factor);
    }

    internal void Move(double dx, double dy)
    {
        for (int i = 0; i < this.points.Count; i++)
            this.points[i] = new Point(this.points[i].X + dx, this.points[i].Y + dy);
    }

    internal void CopyFrom(PolyHelper source)
    {
        System.Diagnostics.Debug.Assert(this.points.Count == 0);
        this.points.AddRange(source.points);
    }

    internal StreamGeometry? BuildGeometry(bool isPolygon)
    {
        if (this.points.Count < 2) { return null; }

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
        return geo;
    }

    internal void Draw(DrawingContext ctx, bool isPolygon, Brush? brush, Pen? pen)
    {
        var geo = this.BuildGeometry(isPolygon);
        if (geo != null)
        {
            ctx.DrawGeometry(brush, pen, geo);
        }
    }
}