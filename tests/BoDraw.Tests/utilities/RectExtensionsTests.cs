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
        var m = sr.TransformInto(tr, flipY: true);

        Assert.Equal(new(100, 126), m.Transform(new(5, 10)));
        Assert.Equal(new(103, 120), m.Transform(new(105, 210)));
    }

    [Fact]
    public void TestTransformIntoKeepAspect()
    {
        // Source 2x4 (aspect 0.5) into target 4x4 (aspect 1) — height-limited, scale=1
        var source = new Rect(0, 0, 2, 4);
        var target = new Rect(10, 20, 4, 4);
        var m = source.TransformInto(target, keepAspect: true);

        // Shape fits as 2x4 region centered in the 4x4 target
        Assert.Equal(new(11, 20), m.Transform(new(0, 0)));
        Assert.Equal(new(13, 24), m.Transform(new(2, 4)));
    }

    [Fact]
    public void TestTransformIntoNoKeepAspect()
    {
        // Source 2x4 stretched to fill 4x4 target — sx=2, sy=1
        var source = new Rect(0, 0, 2, 4);
        var target = new Rect(10, 20, 4, 4);
        var m = source.TransformInto(target, keepAspect: false);

        // Corners map exactly to target corners
        Assert.Equal(new(10, 20), m.Transform(new(0, 0)));
        Assert.Equal(new(14, 24), m.Transform(new(2, 4)));
    }

}
