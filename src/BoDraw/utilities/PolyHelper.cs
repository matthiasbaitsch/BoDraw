using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Manages an ordered list of points and builds Avalonia geometry for polylines and polygons.
/// </summary>
internal class PolyHelper
{
    private readonly List<Point> points = [];

    /// <summary>The axis-aligned bounding box of all stored points.</summary>
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

    /// <summary>Appends a single point (<paramref name="x"/>, <paramref name="y"/>).</summary>
    internal void AddPoint(double x, double y)
    {
        this.points.Add(new Point(x, y));
    }

    /// <summary>Appends points from a flat array of alternating x/y values.</summary>
    internal void AddPoints(double[] coordinates)
    {
        for (int i = 0; i < coordinates.Length - 1; i += 2)
        {
            this.AddPoint(coordinates[i], coordinates[i + 1]);
        }
    }

    /// <summary>Appends points by zipping <paramref name="xs"/> and <paramref name="ys"/>.</summary>
    internal void AddPoints(IEnumerable<double> xs, IEnumerable<double> ys)
    {
        foreach (var (x, y) in xs.Zip(ys))
        {
            this.AddPoint(x, y);
        }
    }

    /// <summary>Transforms all stored points by matrix <paramref name="t"/> in place.</summary>
    internal void ApplyTransform(Matrix t)
    {
        for (int i = 0; i < this.points.Count; i++)
        {
            this.points[i] = this.points[i].Transform(t);
        }
    }

    /// <summary>Copies all points from <paramref name="source"/> into this (empty) helper.</summary>
    internal void CopyFrom(PolyHelper source)
    {
        System.Diagnostics.Debug.Assert(this.points.Count == 0);
        this.points.AddRange(source.points);
    }

    /// <summary>Builds a <see cref="StreamGeometry"/> from the stored points, or returns <c>null</c> if fewer than two points exist.</summary>
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

    /// <summary>Draws the geometry onto <paramref name="ctx"/> using the given <paramref name="brush"/> and <paramref name="pen"/>.</summary>
    internal void Draw(DrawingContext ctx, bool isPolygon, Brush? brush, Pen? pen)
    {
        var geo = this.BuildGeometry(isPolygon);
        if (geo != null)
        {
            ctx.DrawGeometry(brush, pen, geo);
        }
    }
}