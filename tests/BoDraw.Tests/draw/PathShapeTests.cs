using Avalonia;
using Avalonia.Headless.XUnit;

namespace BoDraw.Tests;

public class PathShapeTests
{
    [AvaloniaFact]
    public void TestBounds()
    {
        PathShape p = new PathShape("M 0,0 L 10,0 L 10,5 Z");
        Assert.Equal(new Rect(0, 0, 10, 5), p.Bounds);
    }

    [AvaloniaFact]
    public void TestMove()
    {
        PathShape p = new PathShape("M 0,0 L 10,0 L 10,5 Z");
        p.Move(1, 2);

        Assert.Equal(new Rect(1, 2, 10, 5), p.Bounds);
    }

    [AvaloniaFact]
    public void TestScale()
    {
        PathShape p = new PathShape("M 0,0 L 10,0 L 10,5 Z");
        p.Scale(2, new Point(0, 0));

        Assert.Equal(new Rect(0, 0, 20, 10), p.Bounds);
    }

    [AvaloniaFact]
    public void TestProgrammaticBounds()
    {
        PathShape p = new PathShape();
        p.MoveTo(0, 0);
        p.LineTo(10, 0);
        p.LineTo(10, 5);
        p.Close();

        Assert.Equal(new Rect(0, 0, 10, 5), p.Bounds);
    }

    [AvaloniaFact]
    public void TestProgrammaticMove()
    {
        PathShape p = new PathShape();
        p.MoveTo(0, 0);
        p.LineTo(10, 0);
        p.LineTo(10, 5);
        p.Close();
        p.Move(1, 2);

        Assert.Equal(new Rect(1, 2, 10, 5), p.Bounds);
    }

    [AvaloniaFact]
    public void TestArcTo()
    {
        PathShape p = new PathShape();
        p.MoveTo(0, 20);
        p.ArcTo(20, 0, 20, 20);

        Assert.Equal(new Rect(0, 0, 20, 20), p.Bounds);
    }

    [AvaloniaFact]
    public void TestCurveAndQuadTo()
    {
        PathShape p = new PathShape();
        p.MoveTo(0, 0);
        p.CurveTo(0, 10, 10, 10, 10, 0);
        p.QuadTo(15, 10, 20, 0);

        Assert.Equal(new Rect(0, 0, 20, 7.5), p.Bounds);
    }

    [AvaloniaFact]
    public void TestSegmentWithoutMoveToThrows()
    {
        PathShape p = new PathShape();
        Assert.Throws<InvalidOperationException>(() => p.LineTo(1, 1));
    }

    [AvaloniaFact]
    public void TestCopyIsIndependent()
    {
        PathShape p1 = new PathShape();
        p1.MoveTo(0, 0);
        p1.LineTo(10, 0);
        p1.LineTo(10, 5);

        PathShape p2 = p1.Copy();
        p2.Move(1, 0);

        Assert.Equal(new Rect(0, 0, 10, 5), p1.Bounds);
        Assert.Equal(new Rect(1, 0, 10, 5), p2.Bounds);
    }
}
