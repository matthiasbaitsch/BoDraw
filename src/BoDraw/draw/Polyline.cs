using Avalonia;
using Avalonia.Media;

namespace bodraw;

/// <summary>
/// A connected sequence of line segments defined by an ordered list of points.
/// Points are added via <see cref="AddPoint"/>.
/// </summary>
public class Polyline : LineLikeShape
{
    private readonly PolyHelper helper = new PolyHelper();

    public override Rect Bounds => this.helper.Bounds;

    public void AddPoint(double x, double y) => this.helper.AddPoint(x, y);

    protected override void Draw(DrawingContext ctx, Pen pen)
    {
        this.helper.Draw(ctx, false, null, pen);
    }
}

