using Avalonia;

namespace BoDraw;

internal static class RectExtensions
{
    internal static Rect Move(this Rect r, double dx, double dy)
    {
        return new Rect(r.X + dx, r.Y + dy, r.Width, r.Height);
    }
}
