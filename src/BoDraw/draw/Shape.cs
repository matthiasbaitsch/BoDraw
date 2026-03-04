using Avalonia;
using Avalonia.Media;

namespace bodraw;

public abstract class Shape
{
    public abstract void Draw(DrawingContext ctx);

    public abstract Rect Bounds { get; }
}