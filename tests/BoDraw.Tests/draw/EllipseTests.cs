using Avalonia;
using BoDraw;

namespace BoDraw.Tests;

public class EllipseTests
{
    [Fact]
    public void TestBounds()
    {
        Ellipse e = new Ellipse(5, 6, 2, 3);
        Assert.Equal(new Rect(3, 3, 4, 6), e.Bounds);
    }
}
