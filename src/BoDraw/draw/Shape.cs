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
    /// Applies a matrix transform to all defining points of the shape.
    /// </summary>
    public abstract void ApplyTransform(Matrix t);

    /// <summary>
    /// Default transformation center, defaults to bounds center.
    /// </summary>
    internal virtual Point TransformationCenter { get { return this.Bounds.Center; } }

    protected internal virtual Shape DeepClone()
    {
        return (Shape)this.MemberwiseClone();
    }

    internal abstract void Draw(double scale, DrawingContext ctx);
}
