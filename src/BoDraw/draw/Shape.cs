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

    internal virtual Point ScalingCenter { get { return this.Bounds.Center; } }

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
        this.ApplyTransform(MatrixExtensions.CreateScale(sx, sy, this.ScalingCenter));
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
    /// Rotates the shape by <paramref name="angle"/> degrees counterclockwise around its center.
    /// </summary>
    public Shape Rotate(double angle)
    {
        this.ApplyTransform(Matrix.CreateRotation(angle * Math.PI / 180.0, this.ScalingCenter));
        return this;
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
