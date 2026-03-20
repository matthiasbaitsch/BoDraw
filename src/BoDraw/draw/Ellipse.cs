using Avalonia;
using Avalonia.Media;

namespace bodraw;

public class Ellipse : AreaLikeShape
{
    private Rect rect;

    public Ellipse(double x, double y, double r1, double r2)
    {
        this.rect = new Rect(x - r1, y - r2, 2 * r1, 2 * r2);
    }

    public override Rect Bounds => this.rect;

    protected override void Draw(DrawingContext ctx, Brush? brush, Pen? pen)
    {
        ctx.DrawEllipse(brush, pen, this.rect);
    }
}