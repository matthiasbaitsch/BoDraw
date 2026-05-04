using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Holds a collection of <see cref="Shape"/> objects and renders them onto a target rectangle,
/// automatically scaling and centering the content.
/// </summary>
internal class Drawing
{
    private ShapeCollection shapes = new ShapeCollection();

    internal double PaddingFactor { set; get; } = 0.025;

    internal Color Background { set; get; } = Colors.White;

    internal void Add(Shape[] shapes) { this.shapes.Add(shapes); }

    internal void Clear() { this.shapes.Clear(); }

    internal Rect Bounds { get { return this.shapes.Bounds; } }

    internal void Draw(DrawingContext ctx, Rect targetBounds)
    {
        // Fill background
        ctx.FillRectangle(new SolidColorBrush(this.Background), targetBounds);

        // Quick return
        if (this.shapes.Count == 0) { return; }

        // Transformation
        Rect bounds = this.Bounds;
        Rect paddedTarget = targetBounds.Pad(this.PaddingFactor);
        double scale = Math.Min(paddedTarget.Width / bounds.Width, paddedTarget.Height / bounds.Height);
        Matrix m = bounds.TransformInto(paddedTarget);

        // Draw shapes
        using (ctx.PushTransform(m))
        {
            foreach (Shape s in this.shapes)
            {
                s.Draw(scale, ctx);
            }
        }
    }

}