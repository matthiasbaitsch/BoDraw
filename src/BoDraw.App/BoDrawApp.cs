using Avalonia.Controls;
using System.Diagnostics.CodeAnalysis;

namespace bodraw;

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