using Avalonia;

namespace BoDraw.Tests;

public class GroupTests
{
    [Fact]
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
    public void TestMoveDoesNotMutateChildren()
    {
        var r = new Rectangle(0, 0, 2, 2);
        var g = new Group(r);
        g.Move(3, 4);
        Assert.Equal(new Rect(0, 0, 2, 2), r.Bounds);
    }

    [Fact]
    public void TestCopyIsShallow()
    {
        var r = new Rectangle(0, 0, 2, 2);
        var g = new Group(r);
        var copy = g.Copy(1, 0);
        Assert.Equal(new Rect(1, 0, 2, 2), copy.Bounds);
        Assert.Equal(new Rect(0, 0, 2, 2), g.Bounds);
        // both groups reference the same child
        Assert.Equal(r.Bounds, new Rect(0, 0, 2, 2));
    }
}
