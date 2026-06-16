using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Threading;

namespace BoDraw.Tests;

public static class RenderHelper
{
    public static SettingsTask
    VerifyRendering(Action<BoDrawCanvas> setup, int size = 400)
    {
        var canvas = new BoDrawCanvas();
        setup(canvas);

        var window = new Window
        {
            Content = canvas,
            Width = size,
            Height = size,
            SizeToContent = SizeToContent.Manual
        };
        window.Show();
        Dispatcher.UIThread.RunJobs(DispatcherPriority.Default);

        var bitmap = window.CaptureRenderedFrame()
            ?? throw new InvalidOperationException("CaptureRenderedFrame returned null");

        var path = Path.ChangeExtension(Path.GetTempFileName(), ".png");
        bitmap.Save(path);

        return VerifyFile(path).UseDirectory("Snapshots");
    }
}
