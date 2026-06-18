using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// An axis-aligned rectangle defined by two corner points.
/// </summary>
public class Rectangle : AreaLikeShape
{
    private Rect rectangle;

    /// <summary>
    /// Creates a rectangle from its lower-left and upper-right corner points.
    /// </summary>
    /// <param name="x1">X coordinate of the lower-left corner.</param>
    /// <param name="y1">Y coordinate of the lower-left corner.</param>
    /// <param name="x2">X coordinate of the upper-right corner.</param>
    /// <param name="y2">Y coordinate of the upper-right corner.</param>
    public Rectangle(double x1, double y1, double x2, double y2)
    {
        double x = Math.Min(x1, x2);
        double y = Math.Min(y1, y2);
        double w = Math.Abs(x2 - x1);
        double h = Math.Abs(y2 - y1);
        this.rectangle = new Rect(x, y, w, h);
    }

    public override void Scale(double sx, double sy)
    {
        this.rectangle = this.rectangle.Scale(sx, sy);
    }

    public override void Move(double dx, double dy)
    {
        this.rectangle = this.rectangle.Move(dx, dy);
    }

    public new Rectangle Copy(double dx, double dy)
    {
        return (Rectangle)base.Copy(dx, dy);
    }

    public override Rect Bounds
    {
        get { return this.rectangle; }
    }

    internal override Geometry Geometry
    {
        get { return new RectangleGeometry(this.rectangle); }
    }
}