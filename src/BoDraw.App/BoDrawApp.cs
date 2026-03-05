using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace bodraw;

public class BoDrawApp : IBoDraw
{
    private MainWindow mw;
    private BoDrawCanvas canvas;

    public BoDrawApp()
    {
        this.mw = new MainWindow();
        this.canvas = this.mw.Canvas;
    }

    public Color Background
    {
        get { return this.canvas.Background; }
        set { this.canvas.Background = value; }
    }

    public void Add(params Shape[] shapes)
    {
        this.canvas!.Add(shapes);
    }

    public void Clear()
    {
        this.canvas!.Clear();
    }

    public void Show()
    {
        ModuleInit.AppBuilder!.Instance!.Run(this.mw);
    }
}