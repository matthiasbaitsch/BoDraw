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
        Assert.Equal(5, t.X);
        Assert.Equal(10, t.Y);
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

        // Font family name
        Assert.Equal("Arial", t.FontFamilyName);

        t.FontFamilyName = "Courier New";
        Assert.Equal("Courier New", t.FontFamilyName);
    }

    [AvaloniaFact]
    public void TestBoundsRotated()
    {
        // Rotation is around the anchor point (5, 10), the unrotated box is [5, 5+w] x [10, 10+h]
        var t = new Text("Hello", 5, 10, 12);
        double w = t.Bounds.Width;
        double h = t.Bounds.Height;

        t.Angle = 90;
        AssertRect(new Rect(5 - h, 10, h, w), t.Bounds);

        t.Angle = 180;
        AssertRect(new Rect(5 - w, 10 - h, w, h), t.Bounds);

        t.Angle = -90;
        AssertRect(new Rect(5, 10 - w, h, w), t.Bounds);

        t.Angle = 30;
        double c = Math.Cos(Math.PI / 6);
        double s = Math.Sin(Math.PI / 6);
        AssertRect(new Rect(5 - h * s, 10, w * c + h * s, w * s + h * c), t.Bounds);

        t.Angle = 45;
        Assert.Equal((w + h) / Math.Sqrt(2), t.Bounds.Width, 1e-10);
        Assert.Equal((w + h) / Math.Sqrt(2), t.Bounds.Height, 1e-10);
    }

    [AvaloniaTheory]
    [InlineData(0)]
    [InlineData(30)]
    [InlineData(45)]
    [InlineData(90)]
    public void TestBoundsRotatedCentered(double angle)
    {
        // Centered text is rotated around the center of its box, so the center stays at the anchor
        var t = new Text("Hello", 5, 10, 12).WithJust(0.5, 0.5);
        t.Angle = angle;

        Assert.Equal(5, t.Bounds.Center.X, 1e-10);
        Assert.Equal(10, t.Bounds.Center.Y, 1e-10);
    }

    [AvaloniaFact]
    public void TestBoundsAfterRotate()
    {
        var reference = new Text("Hello", 100, 0, 12);
        double w = reference.Bounds.Width;
        double h = reference.Bounds.Height;

        var t = new Text("Hello", 100, 0, 12).Rotate(90, 0, 0);

        Assert.Equal(0, t.X, 1e-10);
        Assert.Equal(100, t.Y, 1e-10);
        Assert.Equal(90, t.Angle, 1e-10);
        Assert.Equal(12, t.FontSize, 1e-10);
        AssertRect(new Rect(-h, 100, h, w), t.Bounds);
    }

    private static void AssertRect(Rect expected, Rect actual)
    {
        Assert.Equal(expected.X, actual.X, 1e-10);
        Assert.Equal(expected.Y, actual.Y, 1e-10);
        Assert.Equal(expected.Width, actual.Width, 1e-10);
        Assert.Equal(expected.Height, actual.Height, 1e-10);
    }
}
