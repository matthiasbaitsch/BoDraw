using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A line segment with a filled arrowhead at the end point.
/// The arrowhead opens at 30° on each side of the shaft and scales with <see cref="LineLikeShape.Thickness"/>.
/// </summary>
public class Arrow : LineLikeShape
{
    private Point p1;
    private Point p2;

    // Arrowhead length = pen.Thickness (already scale-adjusted) * this factor
    private const double ArrowLengthFactor = 8.0;

    // tan(20°)
    private const double Tan20 = 0.36397023426620099;

    /// <summary>Creates an arrow from (x1,y1) to (x2,y2).</summary>
    public Arrow(double x1, double y1, double x2, double y2)
    {
        this.p1 = new Point(x1, y1);
        this.p2 = new Point(x2, y2);
    }

    public override Rect Bounds
    {
        get
        {
            double x = Math.Min(this.p1.X, this.p2.X);
            double y = Math.Min(this.p1.Y, this.p2.Y);
            double w = Math.Abs(this.p1.X - this.p2.X);
            double h = Math.Abs(this.p1.Y - this.p2.Y);
            return new Rect(x, y, w, h);
        }
    }

    public override void ApplyTransform(Matrix t)
    {
        this.p1 = this.p1.Transform(t);
        this.p2 = this.p2.Transform(t);
    }

    protected override void Draw(DrawingContext ctx, Pen pen)
    {
        double dx = this.p2.X - this.p1.X;
        double dy = this.p2.Y - this.p1.Y;
        double len = Math.Sqrt(dx * dx + dy * dy);

        if (len < 1e-10)
        {
            return;
        }

        // Unit vector along the shaft direction
        double ux = dx / len;
        double uy = dy / len;

        double arrowLen = Math.Min(pen.Thickness * ArrowLengthFactor, len * 0.5);
        double halfWidth = arrowLen * Tan20;

        // Center of the arrowhead base
        double bx = this.p2.X - ux * arrowLen;
        double by = this.p2.Y - uy * arrowLen;

        // Wing points (perpendicular to shaft)
        Point wing1 = new Point(bx - uy * halfWidth, by + ux * halfWidth);
        Point wing2 = new Point(bx + uy * halfWidth, by - ux * halfWidth);

        // Shaft ends at arrowhead base so the tip stays sharp
        ctx.DrawLine(pen, this.p1, new Point(bx, by));

        // Filled arrowhead triangle
        var geo = new StreamGeometry();
        using (var sgc = geo.Open())
        {
            sgc.BeginFigure(this.p2, true);
            sgc.LineTo(wing1);
            sgc.LineTo(wing2);
            sgc.EndFigure(true);
        }
        ctx.DrawGeometry(pen.Brush, null, geo);
    }
}
