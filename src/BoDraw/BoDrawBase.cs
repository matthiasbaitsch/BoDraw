using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Abstract base class that implements <see cref="IBoDraw"/> by delegating to a <see cref="BoDrawCanvas"/>.
/// </summary>
public abstract class BoDrawBase : IBoDraw
{
    public required BoDrawCanvas Canvas;

    /// <summary>Gets or sets the background color of the drawing surface.</summary>
    public Color Background
    {
        get { return this.Canvas.Background; }
        set { this.Canvas.Background = value; }
    }

    public void Add(params Shape[] shapes)
    {
        this.Canvas.Add(shapes);
    }

    public abstract void Animate(double duration, Action<double> frame);

    public void Clear()
    {
        this.Canvas.Clear();
    }
}