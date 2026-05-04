using Avalonia;

namespace BoDraw.Tests;

public class ShapeCollectionTests
{
    [Fact]
    public void TestAdd()
    {
        var sc = new ShapeCollection();
        var r1 = new Rectangle(0, 0, 10, 20);
        var r2 = new Rectangle(5, 5, 15, 25);

        // No duplicates
        sc.Add(r1);
        Assert.Equal(1, sc.Count);
        sc.Add(r2);
        Assert.Equal(2, sc.Count);
        sc.Add(r1);
        Assert.Equal(2, sc.Count);

        // Sequence
        Assert.Same(r2, sc.Get(0));
        Assert.Same(r1, sc.Get(1));

        // Clear
        sc.Clear();
        Assert.Equal(0, sc.Count);
    }

    [Fact]
    public void TestBounds()
    {
        var sc = new ShapeCollection();
        sc.Add(new Rectangle(1, 2, 3, 4));
        sc.Add(new Rectangle(2, 3, 4, 5));
        Assert.Equal(new Rect(1, 2, 3, 3), sc.Bounds);
    }

    [Fact]
    public void TestBoundsEmpty()
    {
        var sc = new ShapeCollection();
        Assert.Equal(new Rect(0, 0, 0, 0), sc.Bounds);
    }

}