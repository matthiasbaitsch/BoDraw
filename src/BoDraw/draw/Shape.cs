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
    public abstract Shape ApplyTransform(Matrix t);

    /// <summary>
    /// Moves the shape by the given offset.
    /// </summary>
    public Shape Move(double dx, double dy)
    {
        return this.ApplyTransform(Matrix.CreateTranslation(dx, dy));
    }

    /// <summary>
    /// Scales the shape by independent factors along each axis around its center.
    /// </summary>
    public Shape Scale(double sx, double sy)
    {
        var c = this.Bounds.Center;
        this.ApplyTransform(
            Matrix.CreateTranslation(-c.X, -c.Y)
                .Append(Matrix.CreateScale(sx, sy))
                .Append(Matrix.CreateTranslation(c.X, c.Y))
        );
        return this;
    }

    /// <summary>
    /// Scales the shape uniformly around its center.
    /// </summary>
    public Shape Scale(double factor)
    {
        return this.Scale(factor, factor);
    }

    /// <summary>
    /// Scales and moves the shape so its bounds fit inside <paramref name="target"/>.
    /// When <paramref name="keepAspect"/> is false the shape is stretched to fill the target exactly;
    /// when true the aspect ratio is preserved and the shape is centered within the target.
    /// </summary>
    public Shape FitInto(Rectangle target, bool keepAspect = false)
    {
        this.ApplyTransform(this.Bounds.TransformInto(target.Bounds, keepAspect));
        return this;
    }

    /// <summary>
    /// Returns a copy of the shape.
    /// </summary>
    public Shape Copy()
    {
        return this.DeepClone();
    }

    /// <summary>
    /// Returns a copy of the shape moved by the given offset.
    /// </summary>
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
