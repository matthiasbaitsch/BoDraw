using Avalonia;
using System;

namespace bodraw;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Polyline pl = new Polyline();
        pl.AddPoint(0, 0);
        pl.AddPoint(200, 0);
        pl.AddPoint(0, 200);
        pl.AddPoint(200, 200);
        pl.AddPoint(0, 0);
        pl.AddPoint(100, -100);
        pl.AddPoint(200, 0);
        pl.AddPoint(200, 200);

        BoDraw bd = new BoDraw();
        bd.Add(pl);
        bd.Show();
    }

    // Make preview happy
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }

}
