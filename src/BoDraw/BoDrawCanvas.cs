using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace bodraw;

/// <summary>
/// An Avalonia <see cref="Control"/> that hosts a <see cref="Drawing"/> and implements <see cref="IBoDraw"/>.
/// Renders all shapes by delegating to <see cref="Drawing.Draw"/>.
/// </summary>
public class BoDrawCanvas : Control, IBoDraw
{

    public Drawing Drawing = new Drawing();

    public Color Background
    {
        get { return this.Drawing.Background; }
        set { this.Drawing.Background = value; }
    }

    public void Add(params Shape[] shapes)
    {
        foreach (Shape s in shapes)
        {
            this.Drawing.Shapes.Add(s);
        }
        this.InvalidateVisual();
    }

    public void Clear()
    {
        this.Drawing.Shapes.Clear();
        this.InvalidateVisual();
    }

    public override void Render(DrawingContext ctx)
    {
        base.Render(ctx);
        this.Drawing.Draw(ctx, this.Bounds);
    }
}