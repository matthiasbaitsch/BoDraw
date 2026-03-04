using Avalonia;

namespace bodraw;

public class BoDraw
{
    private Drawing drawing = new Drawing();

    public BoDraw()
    {
        // AppBuilder.Configure<App>()
        //     .UsePlatformDetect()
        //     .SetupWithoutStarting();
    }

    public void Add(Shape s)
    {
        this.drawing.Shapes.Add(s);
    }

    public void Clear()
    {
        this.drawing.Shapes.Clear();
    }

    public void Show()
    {
        AppBuilder.Configure(
                () => new App(this.drawing)
            )
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace().
            StartWithClassicDesktopLifetime([]);
    }
}