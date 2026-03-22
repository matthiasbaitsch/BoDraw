using Avalonia;
using Avalonia.Media;

namespace bodraw;

public class Polygon : AreaLikeShape
{
    private readonly PolyHelper helper = new PolyHelper();

    public override Rect Bounds => this.helper.Bounds;

    public void AddPoint(double x, double y) => this.helper.AddPoint(x, y);

    protected override void Draw(DrawingContext ctx, Brush? brush, Pen? pen)
    {
        this.helper.Draw(ctx, true, brush, pen);
    }
}

