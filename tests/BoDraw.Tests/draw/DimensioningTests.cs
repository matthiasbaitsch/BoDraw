using Avalonia;

namespace BoDraw.Tests;

public class DimensioningTests
{
    [Fact]
    public void TestDeepClone()
    {
        Dimensioning dim1 = new Dimensioning(0.5);
        dim1.ScalingFactor = 100;
        dim1.Format = "0.##cm";
        dim1.Start(0, -1.5);
        dim1.HStep(6);
        dim1.StartNext();
        dim1.HStep(3, 5, 6);
        dim1.Start(7.5, 0);
        dim1.VStep(6);
        dim1.StartNext();
        dim1.VStep(1, 6);

        Dimensioning dim2 = (Dimensioning)dim1.DeepClone();
        dim2.points[0][0] = new Point(10, 10);

        // Check that we only modified dim2
        Assert.NotEqual(dim1.points[0][0], dim2.points[0][0]);
    }
}
