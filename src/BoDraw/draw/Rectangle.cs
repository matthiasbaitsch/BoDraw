using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// An axis-aligned rectangle defined by two corner points.
/// </summary>
public class Rectangle : AreaLikeShape
{
    private Rect rectangle;

    public Rectangle(double x1, double y1, double x2, double y2)
    {
        double x = Math.Min(x1, x2);
        double y = Math.Min(y1, y2);
        double w = Math.Abs(x2 - x1);
        double h = Math.Abs(y2 - y1);
        this.rectangle = new Rect(x, y, w, h);
    }

    public override Rect Bounds => this.rectangle;

    protected override void Draw(DrawingContext ctx, Brush? brush, Pen? pen)
    {
        ctx.DrawRectangle(brush, pen, this.rectangle);
    }
}