using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

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
        this.drawing.Draw(ctx, new Rect(this.Bounds.Size));
    }

    public void Animate(double duration, Action<double> frame)
    {
        throw new NotImplementedException();
    }

    public void SaveImage(string path, int size = 800)
    {
        var bounds = this.drawing.Bounds;
        int width, height;
        if (bounds.Width >= bounds.Height)
        {
            width = size;
            height = bounds.Height > 0 ? Math.Max(1, (int)Math.Round(size * bounds.Height / bounds.Width)) : size;
        }
        else
        {
            height = size;
            width = bounds.Width > 0 ? Math.Max(1, (int)Math.Round(size * bounds.Width / bounds.Height)) : size;
        }
        var pixelSize = new PixelSize(width, height);
        using var bitmap = new RenderTargetBitmap(pixelSize);
        this.Arrange(new Rect(0, 0, width, height));
        bitmap.Render(this);
        bitmap.Save(path);
    }
}