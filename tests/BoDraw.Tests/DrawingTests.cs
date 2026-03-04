using Avalonia;
using bodraw;

namespace BoDraw.Tests;

public class DrawingTests
{
    [Fact]
    public void TestCreateTransform()
    {
        var sr = new Rect(5, 10, 100, 200);
        var tr = new Rect(100, 120, 3, 6);
        var m = Drawing.CreateTransform(sr, tr, 0.0);
        
        Assert.Equal(tr.Position, m.Transform(new Point(5, 10)));
        Assert.Equal(tr.BottomRight, m.Transform(new Point(105, 210)));
    }
}