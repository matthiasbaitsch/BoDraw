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

    /// <summary>The axis-aligned bounding box of the shape in drawing coordinates.</summary>
    public abstract Rect Bounds { get; }

    /// <summary>
    /// Moves the shape by the given offset.
    /// </summary>
    /// <param name="dx">Horizontal offset.</param>
    /// <param name="dy">Vertical offset.</param>
    public abstract void Move(double dx, double dy);

    /// <summary>
    /// Scales the shape by the given factor around its center.
    /// </summary>
    /// <param name="factor">Scale factor.</param>
    public abstract void Scale(double factor);

    /// <summary>
    /// Returns a copy of the shape moved by the given offset.
    /// </summary>
    /// <param name="dx">Horizontal offset.</param>
    /// <param name="dy">Vertical offset.</param>
    public Shape Copy(double dx, double dy)
    {
        var copy = this.DeepClone();
        copy.Move(dx, dy);
        return copy;
    }

    protected internal virtual Shape DeepClone()
    {
        return (Shape)this.MemberwiseClone();
    }

    internal abstract void Draw(double scale, DrawingContext ctx);
}

