using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A shape that clips one shape to the geometry of another shape.
/// </summary>
public class Clip : Shape
{
    private readonly Shape shape;
    private readonly AreaLikeShape clipShape;

    public Clip(Shape shape, AreaLikeShape clipShape)
    {
        this.shape = shape;
        this.clipShape = clipShape;
    }

    public override Rect Bounds
    {
        get { return this.clipShape.Bounds; }
    }

    public override void ApplyTransform(Matrix t)
    {
        this.shape.ApplyTransform(t);
        this.clipShape.ApplyTransform(t);
    }

    protected internal override Shape DeepClone()
    {
        return new Clip(this.shape, (AreaLikeShape)this.clipShape.DeepClone());
    }

    internal override void Draw(double scale, DrawingContext ctx)
    {
        using (ctx.PushGeometryClip(this.clipShape.Geometry))
        {
            this.shape.Draw(scale, ctx);
        }
    }
}
