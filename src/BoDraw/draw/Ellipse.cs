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

    public override void Scale(double sx, double sy)
    {
        this.rectangle = this.rectangle.Scale(sx, sy);
    }

    public override void Move(double dx, double dy)
    {
        this.rectangle = this.rectangle.Move(dx, dy);
    }

    public new Ellipse Copy(double dx, double dy)
    {
        return (Ellipse)base.Copy(dx, dy);
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

    public new Circle Copy(double dx, double dy)
    {
        return (Circle)base.Copy(dx, dy);
    }
}
