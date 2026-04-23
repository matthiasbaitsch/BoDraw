using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Abstract base class for all drawable shapes.
/// </summary>
public abstract class Shape
{
    internal static Pen? ScalePen(double scale, Pen? pen)
    {
        if (pen == null)
        {
            return null;
        }

        return new Pen(
            pen.Brush,
            pen.Thickness / scale,
            pen.DashStyle,
            pen.LineCap,
            pen.LineJoin,
            pen.MiterLimit
        );
    }

    internal abstract void Draw(double scale, DrawingContext ctx);

    public abstract Rect Bounds { get; }

    public abstract void Move(double dx, double dy);

    public Shape Copy(double dx, double dy)
    {
        var copy = this.DeepClone();
        copy.Move(dx, dy);
        return copy;
    }

    protected virtual Shape DeepClone()
    {
        return (Shape)this.MemberwiseClone();
    }
}