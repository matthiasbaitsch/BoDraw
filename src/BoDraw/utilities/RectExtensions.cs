using Avalonia;

namespace BoDraw;

internal static class RectExtensions
{
    internal static Rect Move(this Rect r, double dx, double dy)
    {
        return new Rect(r.X + dx, r.Y + dy, r.Width, r.Height);
    }

    internal static Rect Scale(this Rect r, double factor)
    {
        double cx = r.X + r.Width / 2;
        double cy = r.Y + r.Height / 2;
        double w = r.Width * factor;
        double h = r.Height * factor;
        return new Rect(cx - w / 2, cy - h / 2, w, h);
    }

    internal static Rect Pad(this Rect r, double factor)
    {
        double p = factor * Math.Min(r.Width, r.Height);
        return new Rect(r.X + p, r.Y + p, r.Width - 2 * p, r.Height - 2 * p);
    }

    internal static Matrix TransformInto(this Rect source, Rect target)
    {
        double scale = Math.Min(target.Width / source.Width, target.Height / source.Height);

        // Transform: Move to center, scale, move to new center
        return
                Matrix.CreateTranslation(-source.Center)
        .Append(Matrix.CreateScale(scale, -scale))
        .Append(Matrix.CreateTranslation(target.Center));
    }
}
