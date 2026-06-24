using Avalonia;

namespace BoDraw;

/// <summary>Extension methods for Avalonia <see cref="Rect"/>.</summary>
internal static class RectExtensions
{
    /// <summary>Returns a new <see cref="Rect"/> with all four corners transformed by <paramref name="t"/>.</summary>
    internal static Rect ApplyTransform(this Rect r, Matrix t)
    {
        var p1 = new Point(r.X, r.Y).Transform(t);
        var p2 = new Point(r.Right, r.Bottom).Transform(t);
        return new Rect(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
    }

    /// <summary>Shrinks the rectangle by <paramref name="factor"/> times its shorter side on each edge.</summary>
    internal static Rect Pad(this Rect r, double factor)
    {
        double p = factor * Math.Min(r.Width, r.Height);
        return new Rect(r.X + p, r.Y + p, r.Width - 2 * p, r.Height - 2 * p);
    }

    /// <summary>
    /// Returns a matrix that maps <paramref name="source"/> into <paramref name="target"/>,
    /// optionally preserving aspect ratio and flipping the Y axis.
    /// </summary>
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
