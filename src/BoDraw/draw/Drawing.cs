using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace bodraw;

public class Drawing
{

    private static double ComputeScalingFactor(Rect sourceBounds, Rect targetBounds, double paddingFactor)
    {
        // Source
        double sw = sourceBounds.Width;
        double sh = sourceBounds.Height;

        // Target
        double padding = paddingFactor * Math.Min(targetBounds.Width, targetBounds.Height);
        double tw = targetBounds.Width - 2 * padding;
        double th = targetBounds.Height - 2 * padding;

        // Scaling factor
        return Math.Min(tw / sw, th / sh);
    }

    public static Matrix CreateTransform(Rect sourceBounds, Rect targetBounds, double pf)
    {
        double scale = ComputeScalingFactor(sourceBounds, targetBounds, pf);

        // Transform: Move to center, scale, move to new center
        return
                Matrix.CreateTranslation(-sourceBounds.Center)
        .Append(Matrix.CreateScale(scale, -scale))
        .Append(Matrix.CreateTranslation(targetBounds.Center));
    }

    public List<Shape> Shapes = [];
    public double PaddingFactor = 0.025;
    public Color Background = Colors.WhiteSmoke;

    public Rect Bounds
    {
        get
        {
            Rect b = new Rect(0, 0, 0, 0);
            foreach (Shape s in this.Shapes)
            {
                b = b.Union(s.Bounds);
            }
            return b;
        }
    }

    public void Draw(DrawingContext ctx, Rect targetBounds)
    {
        // Fill background
        ctx.FillRectangle(new SolidColorBrush(this.Background), targetBounds);

        // Quick return
        if (this.Shapes.Count == 0) { return; }

        // Compute scaling factor
        Rect sourceBounds = this.Bounds;
        double scale = Drawing.ComputeScalingFactor(sourceBounds, targetBounds, this.PaddingFactor);

        // Draw shapes
        using (ctx.PushTransform(Drawing.CreateTransform(sourceBounds, targetBounds, this.PaddingFactor)))
        {
            foreach (Shape s in this.Shapes)
            {
                s.Draw(scale, ctx);
            }
        }
    }
}