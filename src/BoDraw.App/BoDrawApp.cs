using Avalonia.Controls;
using System.Diagnostics.CodeAnalysis;

namespace bodraw;

/// <summary>
/// Desktop application entry point. Opens an Avalonia <see cref="MainWindow"/> and runs the
/// event loop when <see cref="Show"/> is called.
/// </summary>
public class BoDrawApp : BoDrawBase
{
    private MainWindow mw;

    [SetsRequiredMembers]
    public BoDrawApp()
    {
        this.mw = new MainWindow();
        this.Canvas = this.mw.Canvas;
    }

    public void Show()
    {
        ModuleInit.AppBuilder!.Instance!.Run(this.mw);
    }
}