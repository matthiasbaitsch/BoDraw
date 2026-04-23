using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A polygon defined by an ordered list of points.
/// Points are added via <see cref="AddPoint"/>.
/// </summary>
public class Polygon : AreaLikeShape
{
    private PolyHelper helper = new PolyHelper();

    public override Rect Bounds => this.helper.Bounds;

    public void AddPoint(double x, double y) => this.helper.AddPoint(x, y);

    public override void Scale(double factor)
    {
        this.helper.Scale(factor);
    }

    public override void Move(double dx, double dy)
    {
        this.helper.Move(dx, dy);
    }

    protected override Shape DeepClone()
    {
        var copy = (Polygon)base.DeepClone();
        var fresh = new PolyHelper();
        fresh.CopyFrom(this.helper);
        copy.helper = fresh;
        return copy;
    }

    protected override void Draw(DrawingContext ctx, Brush? brush, Pen? pen)
    {
        this.helper.Draw(ctx, true, brush, pen);
    }
}

