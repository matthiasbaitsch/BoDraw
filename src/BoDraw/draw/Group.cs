using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A shape that groups other shapes.
/// </summary>
public class Group : Shape
{
    private readonly ShapeCollection shapes = new ShapeCollection();
    private Matrix transform = Matrix.Identity;

    public Group(params Shape[] shapes)
    {
        this.shapes.Add(shapes);
    }

    private Group(Shape[] shapes, Matrix transform)
    {
        this.shapes.Add(shapes);
        this.transform = transform;
    }

    public void Add(params Shape[] shapes)
    {
        this.shapes.Add(shapes);
    }

    private Rect LocalBounds { get { return this.shapes.Bounds; } }

    public override Rect Bounds
    {
        get { return this.LocalBounds.TransformToAABB(this.transform); }
    }

    public override void ApplyTransform(Matrix t)
    {
        this.transform = this.transform.Append(t);
    }

    protected internal override Shape DeepClone()
    {
        return new Group(this.shapes.ToArray(), this.transform);
    }

    public new Group Copy(double dx, double dy)
    {
        return (Group)base.Copy(dx, dy);
    }

    internal override void Draw(double scale, DrawingContext ctx)
    {
        using (ctx.PushTransform(this.transform))
        {
            foreach (var s in this.shapes)
            {
                s.Draw(scale, ctx);
            }
        }
    }
}
