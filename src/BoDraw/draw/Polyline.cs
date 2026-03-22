using Avalonia;
using Avalonia.Media;

namespace bodraw;

/// <summary>
/// A connected sequence of line segments defined by an ordered list of points.
/// Points are added via <see cref="AddPoint"/>.
/// </summary>
public class Polyline : LineLikeShape
{
    private readonly List<Point> points = [];

    public override Rect Bounds
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

    public void AddPoint(double x, double y)
    {
        this.points.Add(new Point(x, y));
    }

    protected override void Draw(DrawingContext ctx, Pen pen)
    {
        // Quick return
        if (this.points.Count < 2) { return; }

        // Construct geometry
        var geo = new StreamGeometry();
        using (var sgc = geo.Open())
        {
            sgc.BeginFigure(this.points[0], false);
            for (int i = 1; i < this.points.Count; i++)
            {
                sgc.LineTo(this.points[i]);
            }
            sgc.EndFigure(false);
        }

        // Draw geometry
        ctx.DrawGeometry(null, pen, geo);
    }
}

