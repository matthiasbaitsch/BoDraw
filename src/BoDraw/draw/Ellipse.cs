using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// An ellipse defined by a center point and two radii.
/// </summary>
public class Ellipse : AreaLikeShape
{
    private Rect rectangle;

    public Ellipse(double x, double y, double r1, double r2)
    {
        this.rectangle = new Rect(x - r1, y - r2, 2 * r1, 2 * r2);
    }

    public override void ApplyTransform(Matrix t)
    {
        this.rectangle = this.rectangle.ApplyTransform(t);
    }

    public override Rect Bounds
    {
        get { return this.rectangle; }
    }

    internal override Geometry Geometry
    {
        get { return new EllipseGeometry(this.rectangle); }
    }
}

/// <summary>
/// A circle defined by a center point and a single radius.
/// </summary>
public class Circle : Ellipse
{
    public Circle(double x, double y, double r) : base(x, y, r, r) { }

}
