using BoDraw;
using Avalonia;
using Avalonia.Headless.XUnit;

namespace BoDraw.Tests;

public class TextTests
{
    [AvaloniaFact]
    public void TestBounds()
    {
        var t = new Text("Hello", 5, 10, 12);
        Rect bounds = t.Bounds;

        // Left/bottom aligned: position is top-left corner of text
        Assert.Equal(5, bounds.X);
        Assert.Equal(10, bounds.Y);
        Assert.True(bounds.Width > 0);
        Assert.True(bounds.Height > 0);

        double w = bounds.Width;
        double h = bounds.Height;

        // Centered: position shifts by half width/height
        t.HJust = 0.5;
        t.VJust = 0.5;
        bounds = t.Bounds;
        Assert.Equal(5 - w / 2, bounds.X, 1e-10);
        Assert.Equal(10 - h / 2, bounds.Y, 1e-10);
        Assert.Equal(w, bounds.Width, 1e-10);
        Assert.Equal(h, bounds.Height, 1e-10);
    }
}
