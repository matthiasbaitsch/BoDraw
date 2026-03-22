using Avalonia;
using BoDraw;

namespace BoDraw.Tests;

public class PolylineTests
{
    [Fact]
    public void TestBoundsEmpty()
    {
        Polyline p = new Polyline();
        Assert.Equal(new Rect(0, 0, 0, 0), p.Bounds);
    }

    [Fact]
    public void TestBounds()
    {
        Polyline p1 = new Polyline();
        p1.AddPoint(1, 2);
        p1.AddPoint(3, 4);

        Polyline p2 = new Polyline();
        p2.AddPoint(3, 4);
        p2.AddPoint(1, 2);

        Assert.Equal(new Rect(1, 2, 2, 2), p1.Bounds);
        Assert.Equal(new Rect(1, 2, 2, 2), p2.Bounds);
    }
}
