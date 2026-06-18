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

    public override void Move(double dx, double dy)
    {
        this.transform = this.transform.Append(Matrix.CreateTranslation(dx, dy));
    }

    public override void Scale(double sx, double sy)
    {
        var c = this.Bounds.Center;
        this.transform = this.transform
            .Append(Matrix.CreateTranslation(-c.X, -c.Y))
            .Append(Matrix.CreateScale(sx, sy))
            .Append(Matrix.CreateTranslation(c.X, c.Y));
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
