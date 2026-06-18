using Avalonia;

namespace BoDraw;

internal static class RectExtensions
{
    internal static Rect ApplyTransform(this Rect r, Matrix t)
    {
        var p1 = new Point(r.X, r.Y).Transform(t);
        var p2 = new Point(r.Right, r.Bottom).Transform(t);
        return new Rect(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
    }

    internal static Rect Pad(this Rect r, double factor)
    {
        double p = factor * Math.Min(r.Width, r.Height);
        return new Rect(r.X + p, r.Y + p, r.Width - 2 * p, r.Height - 2 * p);
    }

    internal static Matrix TransformInto(this Rect source, Rect target, bool keepAspect = true, bool flipY = false)
    {
        double sx, sy;
        if (keepAspect)
        {
            double scale = Math.Min(target.Width / source.Width, target.Height / source.Height);
            sx = scale;
            sy = scale;
        }
        else
        {
            sx = target.Width / source.Width;
            sy = target.Height / source.Height;
        }
        return Matrix.CreateTranslation(-source.Center)
            .Append(Matrix.CreateScale(sx, flipY ? -sy : sy))
            .Append(Matrix.CreateTranslation(target.Center));
    }
}
