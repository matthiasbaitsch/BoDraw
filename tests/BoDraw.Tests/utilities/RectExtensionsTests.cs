using Avalonia;

namespace BoDraw.Tests;

public class RectExtensionsTests
{
    [Fact]
    public void TestPad()
    {
        Assert.Equal(new Rect(1, 1, 8, 18), new Rect(0, 0, 10, 20).Pad(0.1));
        Assert.Equal(new Rect(1, 1, 18, 8), new Rect(0, 0, 20, 10).Pad(0.1));
    }

    [Fact]
    public void TestTransformInto()
    {
        var sr = new Rect(5, 10, 100, 200);
        var tr = new Rect(100, 120, 3, 6);
        var m = sr.TransformInto(tr);

        Assert.Equal(new(100, 126), m.Transform(new(5, 10)));
        Assert.Equal(new(103, 120), m.Transform(new(105, 210)));
    }

}
