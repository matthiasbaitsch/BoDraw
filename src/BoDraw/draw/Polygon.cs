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

    /// <summary>Creates an empty polygon</summary>
    public Polygon() { }

    /// <summary>Creates a polygon from a flat coordinate list: x0, y0, x1, y1, …</summary>
    public Polygon(params double[] coordinates)
    {
        this.helper.AddPoints(coordinates);
    }

    /// <summary>
    /// Creates a polygon from data sequences. When <paramref name="bounds"/> is provided,
    /// values are mapped from their natural range into the rectangle; otherwise the data
    /// values are used directly as drawing coordinates.
    /// </summary>
    public Polygon(IEnumerable<double> xs, IEnumerable<double> ys, Rectangle? bounds = null)
    {
        this.helper.AddPoints(xs, ys, bounds);
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

    internal override Geometry Geometry
    {
        get { return this.helper.BuildGeometry(true) ?? new StreamGeometry(); }
    }
}

