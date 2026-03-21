using bodraw;
using Avalonia;
using Avalonia.Headless.XUnit;

namespace BoDraw.Tests;

public class TextTests
{
    [AvaloniaFact]
    public void TestBounds()
    {
        // Default
        var t = new Text("Hello", 5, 10, 10 / 1.14990234375);
        Rect bounds = t.Bounds;
        Assert.Equal(5, bounds.X);
        Assert.Equal(10, t.Bounds.Y);
        Assert.Equal(50, t.Bounds.Width);
        Assert.Equal(10, t.Bounds.Height);

        // Centered
        t.HJust = 0.5;
        t.VJust = 0.5;
        bounds = t.Bounds;
        Assert.Equal(-20, bounds.X);
        Assert.Equal(5, t.Bounds.Y);
        Assert.Equal(50, t.Bounds.Width);
        Assert.Equal(10, t.Bounds.Height);
    }
}
