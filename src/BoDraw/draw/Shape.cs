using System.Dynamic;
using Avalonia;
using Avalonia.Media;

namespace bodraw;

public abstract class Shape
{
    internal static Pen ScalePen(double a, Pen pen)
    {
        return new Pen(
            pen.Brush,
            pen.Thickness / a,
            pen.DashStyle,
            pen.LineCap,
            pen.LineJoin,
            pen.MiterLimit
        );
    }

    public abstract void Draw(double a, DrawingContext ctx);

    public abstract Rect Bounds { get; }
}