using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A straight line segment between two points.
/// </summary>
public class Line : LineLikeShape
{

    private Point p1;
    private Point p2;

    public Line(double x1, double y1, double x2, double y2)
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

    public override void Scale(double factor)
    {
        var c = this.Bounds.Center;
        this.p1 = new Point(c.X + (this.p1.X - c.X) * factor, c.Y + (this.p1.Y - c.Y) * factor);
        this.p2 = new Point(c.X + (this.p2.X - c.X) * factor, c.Y + (this.p2.Y - c.Y) * factor);
    }

    public override void Move(double dx, double dy)
    {
        this.p1 = new Point(this.p1.X + dx, this.p1.Y + dy);
        this.p2 = new Point(this.p2.X + dx, this.p2.Y + dy);
    }

    public new Line Copy(double dx, double dy)
    {
        return (Line)base.Copy(dx, dy);
    }

    protected override void Draw(DrawingContext ctx, Pen pen)
    {
        ctx.DrawLine(pen, this.p1, this.p2);
    }
}