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

    public override Rect Bounds
    {
        get { return this.helper.Bounds; }
    }

    /// <summary>Creates a polygon from a flat coordinate list: x0, y0, x1, y1, …</summary>
    public Polygon(params double[] coordinates)
    {
        for (int i = 0; i < coordinates.Length - 1; i += 2)
        {
            this.AddPoint(coordinates[i], coordinates[i + 1]);
        }
    }

    public void AddPoint(double x, double y)
    {
        this.helper.AddPoint(x, y);
    }

    public override void Scale(double factor)
    {
        this.helper.Scale(factor);
    }

    public override void Move(double dx, double dy)
    {
        this.helper.Move(dx, dy);
    }

    protected internal override Shape DeepClone()
    {
        var copy = (Polygon)base.DeepClone();
        var fresh = new PolyHelper();
        fresh.CopyFrom(this.helper);
        copy.helper = fresh;
        return copy;
    }

    public new Polygon Copy(double dx, double dy)
    {
        return (Polygon)base.Copy(dx, dy);
    }

    protected override void Draw(DrawingContext ctx, Brush? brush, Pen? pen)
    {
        this.helper.Draw(ctx, true, brush, pen);
    }
}

