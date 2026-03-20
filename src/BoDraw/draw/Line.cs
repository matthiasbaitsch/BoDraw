using Avalonia;
using Avalonia.Media;

namespace bodraw;

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

    protected override void Draw(DrawingContext ctx, Pen pen)
    {
        ctx.DrawLine(pen, this.p1, this.p2);
    }
}