using Avalonia;
using Avalonia.Controls;

namespace bodraw;

public class BoDraw
{
    private AppBuilder appBuilder;
    private Drawing drawing = new Drawing();

    public BoDraw()
    {
        this.appBuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .SetupWithoutStarting();
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
        this.appBuilder.Instance!.Run(new MainWindow(this.drawing));
    }
}