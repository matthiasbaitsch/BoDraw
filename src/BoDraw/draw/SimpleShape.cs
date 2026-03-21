
using Avalonia;
using Avalonia.Media;

namespace bodraw;

public abstract class SimpleShape : Shape
{
    internal override sealed void Draw(double scale, DrawingContext ctx)
    {
        this.Draw(ctx);
    }

    protected abstract void Draw(DrawingContext ctx);
}