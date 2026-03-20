using Avalonia;
using Avalonia.Media;

namespace bodraw;

public class Rectangle : AreaLikeShape
{
    private Rect rect;

    public Rectangle(double x1, double y1, double x2, double y2)
    {
        double x = Math.Min(x1, x2);
        double y = Math.Min(y1, y2);
        double w = Math.Abs(x2 - x1);
        double h = Math.Abs(y2 - y1);

        this.rect = new Rect(x, y, w, h);
    }

    public override Rect Bounds => this.rect;

    protected override void Draw(DrawingContext ctx, Brush? brush, Pen? pen)
    {
        ctx.DrawRectangle(brush, pen, this.rect);
    }
}