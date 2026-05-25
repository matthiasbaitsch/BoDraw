using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// An ellipse defined by a center point and two radii.
/// </summary>
public class Ellipse : AreaLikeShape
{
    private Rect rect;

    public Ellipse(double x, double y, double r1, double r2)
    {
        this.rect = new Rect(x - r1, y - r2, 2 * r1, 2 * r2);
    }

    public override void Scale(double factor)
    {
        this.rect = this.rect.Scale(factor);
    }

    public override void Move(double dx, double dy)
    {
        this.rect = this.rect.Move(dx, dy);
    }

    public new Ellipse Copy(double dx, double dy)
    {
        return (Ellipse)base.Copy(dx, dy);
    }

    public override Geometry Geometry
    {
        get { return new EllipseGeometry(this.rect); }
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
