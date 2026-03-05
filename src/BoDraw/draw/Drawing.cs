using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace bodraw;

public class Drawing
{

    public static Matrix CreateTransform(Rect sourceBounds, Rect targetBounds, double pf)
    {
        // Source
        double sw = sourceBounds.Width;
        double sh = sourceBounds.Height;

        // Target
        double padding = pf * Math.Min(targetBounds.Width, targetBounds.Height);
        double tw = targetBounds.Width - 2 * padding;
        double th = targetBounds.Height - 2 * padding;

        // Scaling factor
        double s = Math.Min(tw / sw, th / sh);

        // Transform: Move to center, scale, move to new center
        return
                Matrix.CreateTranslation(-sourceBounds.Center)
        .Append(Matrix.CreateScale(s, s))
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
        ctx.FillRectangle(new SolidColorBrush(this.Background), targetBounds);

        using (ctx.PushTransform(CreateTransform(this.Bounds, targetBounds, this.PaddingFactor)))
        {
            foreach (Shape s in this.Shapes)
            {
                s.Draw(ctx);
            }
        }
    }
}