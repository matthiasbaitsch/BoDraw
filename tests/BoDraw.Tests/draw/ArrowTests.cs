using Avalonia;

namespace BoDraw.Tests;

public class ArrowTests
{
    [Fact]
    public void TestBounds()
    {
        Arrow a1 = new Arrow(1, 2, 3, 4);
        Arrow a2 = new Arrow(3, 4, 1, 2);

        Assert.Equal(a1.Bounds, new Rect(1, 2, 2, 2));
        Assert.Equal(a2.Bounds, new Rect(1, 2, 2, 2));
    }

    [Fact]
    public void TestMove()
    {
        Arrow a = new Arrow(0, 0, 4, 3);
        a.Move(1, 2);

        Assert.Equal(a.Bounds, new Rect(1, 2, 4, 3));
    }

    [Fact]
    public void TestScale()
    {
        Arrow a = new Arrow(0, 0, 4, 0);
        a.Scale(2);

        Assert.Equal(a.Bounds, new Rect(-2, 0, 8, 0));
    }
}
