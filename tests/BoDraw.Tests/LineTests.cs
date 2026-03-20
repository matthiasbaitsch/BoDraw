using Avalonia;
using bodraw;

namespace BoDraw.Tests;

public class LineTests
{
    [Fact]
    public void TestBounds()
    {
        Line l1 = new Line(1, 2, 3, 4);
        Line l2 = new Line(3, 4, 1, 2);

        Assert.Equal(l1.Bounds, new Rect(1, 2, 2, 2));
        Assert.Equal(l2.Bounds, new Rect(1, 2, 2, 2));
    }
}