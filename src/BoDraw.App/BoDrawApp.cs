using Avalonia.Controls;
using Avalonia.Media;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BoDraw;

/// <summary>
/// Desktop application entry point. Opens an Avalonia <see cref="MainWindow"/> and runs the
/// event loop when <see cref="Show"/> is called.
/// </summary>
public class BoDrawApp : IBoDraw
{
    private MainWindow mw;

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

    public void Clear()
    {
        this.Canvas.Clear();
    }

    /// <summary>Creates a new application instance and initializes the main window.</summary>
    [SetsRequiredMembers]
    public BoDrawApp()
    {
        this.mw = new MainWindow();
        this.Canvas = this.mw.Canvas;
    }

    /// <summary>Runs an animation by repeatedly invoking <paramref name="frame"/> over <paramref name="duration"/> seconds.</summary>
    public void Animate(double duration, Action<double> frame)
    {
        this.mw.Animate(duration, frame);
    }

    /// <summary>Opens the main window and starts the Avalonia event loop.</summary>
    public void Show()
    {
        ModuleInit.AppBuilder!.Instance!.Run(this.mw);
    }

    /// <summary>
    /// Saves the current drawing as a PNG at <paramref name="path"/> relative to the caller's source file.
    /// </summary>
    public void SaveImage(string path, int size = 800, [CallerFilePath] string callerFilePath = "")
    {
        string directory = Path.GetDirectoryName(callerFilePath) ?? ".";
        this.Canvas.SaveImage(Path.Combine(directory, path), size);
    }
}