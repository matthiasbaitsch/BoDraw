using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace bodraw;

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
    }

    public void Clear()
    {
        this.Drawing.Shapes.Clear();
    }

    public override void Render(DrawingContext ctx)
    {
        base.Render(ctx);
        this.Drawing.Draw(ctx, this.Bounds);
    }
}