using Avalonia;
using Avalonia.Headless.XUnit;

namespace BoDraw.Tests;

public class GroupTests
{
    [AvaloniaFact]
    public void TestBounds()
    {
        var g = new Group(new Rectangle(0, 0, 2, 2), new Rectangle(4, 4, 6, 6));
        Assert.Equal(new Rect(0, 0, 6, 6), g.Bounds);
    }

    [Fact]
    public void TestMove()
    {
        var g = new Group(new Rectangle(0, 0, 2, 2));
        g.Move(3, 4);
        Assert.Equal(new Rect(3, 4, 2, 2), g.Bounds);
    }

    [Fact]
    public void TestCopy()
    {
        var r = new Rectangle(0, 0, 2, 2);
        var g1 = new Group(r);
        var g2 = g1.Copy(1, 0);
        Assert.Equal(r.Bounds, new Rect(0, 0, 2, 2));
        Assert.Equal(new Rect(1, 0, 2, 2), g2.Bounds);
        Assert.Equal(new Rect(0, 0, 2, 2), g1.Bounds);
    }
}
