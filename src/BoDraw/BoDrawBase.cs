using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Abstract base class that implements <see cref="IBoDraw"/> by delegating to a <see cref="BoDrawCanvas"/>.
/// </summary>
public abstract class BoDrawBase : IBoDraw
{
    public required BoDrawCanvas Canvas;

    public Color Background
    {
        get { return this.Canvas.Background; }
        set { this.Canvas.Background = value; }
    }

    public void Add(params Shape[] shapes)
    {
        this.Canvas.Add(shapes);
    }

    public void Clear()
    {
        this.Canvas.Clear();
    }
}