using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A connected sequence of line segments defined by an ordered list of points.
/// Points are added via <see cref="AddPoint"/>.
/// </summary>
public class Polyline : LineLikeShape
{
    private PolyHelper helper = new PolyHelper();

    /// <summary>Creates an empty polyline</summary>
    public Polyline() { }

    /// <summary>Creates a polyline from a flat coordinate list: x0, y0, x1, y1, …</summary>
    public Polyline(params double[] coordinates)
    {
        for (int i = 0; i < coordinates.Length - 1; i += 2)
        {
            this.AddPoint(coordinates[i], coordinates[i + 1]);
        }
    }

    public override Rect Bounds
    {
        get { return this.helper.Bounds; }
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
        var copy = (Polyline)base.DeepClone();
        var fresh = new PolyHelper();
        fresh.CopyFrom(this.helper);
        copy.helper = fresh;
        return copy;
    }

    public new Polyline Copy(double dx, double dy)
    {
        return (Polyline)base.Copy(dx, dy);
    }

    protected override void Draw(DrawingContext ctx, Pen pen)
    {
        this.helper.Draw(ctx, false, null, pen);
    }
}

