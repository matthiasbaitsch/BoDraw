using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A shape that groups other shapes.
/// </summary>
public class Group : Shape
{
    private readonly ShapeCollection shapes = new ShapeCollection();

    public Group(params Shape[] shapes)
    {
        this.shapes.Add(shapes);
    }

    public void Add(params Shape[] shapes)
    {
        this.shapes.Add(shapes);
    }

    public override Rect Bounds { get { return this.shapes.Bounds; } }

    public override void ApplyTransform(Matrix t)
    {
        foreach (Shape s in this.shapes)
        {
            s.ApplyTransform(t);
        }
    }

    protected internal override Shape DeepClone()
    {
        Shape[] shapes = new Shape[this.shapes.Count];
        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i] = this.shapes.Get(i).Copy();
        }
        return new Group(shapes);
    }

    internal override void Draw(double scale, DrawingContext ctx)
    {
        foreach (var s in this.shapes)
        {
            s.Draw(scale, ctx);
        }
    }
}
