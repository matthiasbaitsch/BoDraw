using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace bodraw;

public class DrawingCanvas : Control
{

    public Drawing Drawing = new Drawing();

    public override void Render(DrawingContext ctx)
    {
        base.Render(ctx);
        this.Drawing.Draw(ctx, this.Bounds);
    }
}