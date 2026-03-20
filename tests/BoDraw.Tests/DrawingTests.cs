using Avalonia;
using bodraw;

namespace BoDraw.Tests;

public class DrawingTests
{
    [Fact]
    public void TestBoundsEmpty()
    {
        Drawing d = new Drawing();
        Assert.Equal(new Rect(0, 0, 0, 0), d.Bounds);
    }

    [Fact]
    public void TestCreateTransform()
    {
        // [5, 10] x [105, 210]
        var sr = new Rect(5, 10, 100, 200);
        // [100, 120] x [103, 126]
        var tr = new Rect(100, 120, 3, 6);
        // Transform
        var m = Drawing.CreateTransform(sr, tr, 0.0);

        Assert.Equal(new(100, 126), m.Transform(new(5, 10)));
        Assert.Equal(new(103, 120), m.Transform(new(105, 210)));
    }
}