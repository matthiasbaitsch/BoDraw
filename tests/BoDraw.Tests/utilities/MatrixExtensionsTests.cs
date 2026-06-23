using Avalonia;

namespace BoDraw.Tests;

public class MatrixExtensionsTests
{
    [Fact]
    public void TestScale()
    {
        Point p1 = new Point(2, 2);
        Matrix t = MatrixExtensions.CreateScale(2, 3, new Point(1, 1));
        Point p2 = p1.Transform(t);
        Assert.Equal(new Point(3, 4), p2);
    }

}
