
using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Abstract base class for shapes that manage their own rendering without a pen or brush
/// (e.g. <see cref="Text"/>, <see cref="Image"/>). The scale parameter is ignored during drawing.
/// </summary>
public abstract class SimpleShape : Shape
{
    internal override sealed void Draw(double scale, DrawingContext ctx)
    {
        this.Draw(ctx);
    }

    protected abstract void Draw(DrawingContext ctx);
}