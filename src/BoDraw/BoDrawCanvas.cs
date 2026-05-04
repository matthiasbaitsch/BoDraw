using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// An Avalonia <see cref="Control"/> that hosts a <see cref="Drawing"/> and implements <see cref="IBoDraw"/>.
/// Renders all shapes by delegating to <see cref="Drawing.Draw"/>.
/// </summary>
public class BoDrawCanvas : Control, IBoDraw
{

    private Drawing drawing = new Drawing();

    public Color Background
    {
        get { return this.drawing.Background; }
        set { this.drawing.Background = value; }
    }

    public void Add(params Shape[] shapes)
    {
        this.drawing.Add(shapes);
        this.InvalidateVisual();
    }

    public void Clear()
    {
        this.drawing.Clear();
        this.InvalidateVisual();
    }

    public override void Render(DrawingContext ctx)
    {
        base.Render(ctx);
        this.drawing.Draw(ctx, this.Bounds);
    }
}