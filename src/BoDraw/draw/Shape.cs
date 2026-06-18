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
    /// Scales the shape by independent factors along each axis around its center.
    /// </summary>
    /// <param name="sx">Horizontal scale factor.</param>
    /// <param name="sy">Vertical scale factor.</param>
    public abstract void Scale(double sx, double sy);

    /// <summary>
    /// Scales the shape uniformly around its center.
    /// </summary>
    /// <param name="factor">Scale factor.</param>
    public void Scale(double factor)
    {
        this.Scale(factor, factor);
    }

    /// <summary>
    /// Scales and moves the shape so its bounds fit inside <paramref name="target"/>.
    /// When <paramref name="keepAspect"/> is false the shape is stretched to fill the target exactly;
    /// when true the aspect ratio is preserved and the shape is centered within the target.
    /// </summary>
    /// <param name="target">The rectangle to fit into.</param>
    /// <param name="keepAspect">Whether to preserve the aspect ratio.</param>
    public Shape FitInto(Rectangle target, bool keepAspect = false)
    {
        var t = target.Bounds;
        var b = this.Bounds;
        if (keepAspect)
        {
            double factor = Math.Min(t.Width / b.Width, t.Height / b.Height);
            this.Scale(factor);
        }
        else
        {
            this.Scale(t.Width / b.Width, t.Height / b.Height);
        }
        var c = this.Bounds.Center;
        this.Move(t.Center.X - c.X, t.Center.Y - c.Y);
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

