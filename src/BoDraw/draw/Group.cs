using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A shape that groups other shapes.
/// </summary>
public class Group : Shape
{
    private readonly List<Shape> shapes;
    private Matrix transform = Matrix.Identity;

    public Group(params Shape[] shapes)
    {
        this.shapes = [.. shapes];
    }

    private Group(List<Shape> shapes, Matrix transform)
    {
        this.shapes = shapes;
        this.transform = transform;
    }

    private Rect LocalBounds
    {
        get
        {
            Rect b = new Rect(0, 0, 0, 0);
            foreach (var s in this.shapes)
            {
                b = b.Union(s.Bounds);
            }
            return b;
        }
    }

    public override Rect Bounds
    {
        get { return this.LocalBounds.TransformToAABB(this.transform); }
    }

    public override void Move(double dx, double dy)
    {
        this.transform = this.transform.Append(Matrix.CreateTranslation(dx, dy));
    }

    public override void Scale(double factor)
    {
        var c = this.Bounds.Center;
        this.transform = this.transform
            .Append(Matrix.CreateTranslation(-c.X, -c.Y))
            .Append(Matrix.CreateScale(factor, factor))
            .Append(Matrix.CreateTranslation(c.X, c.Y));
    }

    protected internal override Shape DeepClone()
    {
        return new Group([.. this.shapes], this.transform);
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
