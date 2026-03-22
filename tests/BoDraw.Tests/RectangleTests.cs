using Avalonia;
using BoDraw;

namespace BoDraw.Tests;

public class RectangleTests
{
    [Fact]
    public void TestBounds()
    {
        Rectangle r1 = new Rectangle(1, 2, 3, 4);
        Rectangle r2 = new Rectangle(3, 4, 1, 2);
        Assert.Equal(r1.Bounds, new Rect(1, 2, 2, 2));
        Assert.Equal(r2.Bounds, new Rect(1, 2, 2, 2));
    }
}