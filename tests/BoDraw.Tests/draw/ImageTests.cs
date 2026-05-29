using BoDraw;
using Avalonia;
using Avalonia.Headless.XUnit;

namespace BoDraw.Tests;

public class ImageTests
{
    [AvaloniaFact]
    public void TestPixel()
    {
        Image image = new Image(0, 0, 15, 55, 3);
        Image.Pixel p = image.PixelAt(5, 1);

        Assert.Equal(15, image.Width);
        Assert.Equal(55, image.Height);
        Assert.Equal(0.5, p.NRow);
        Assert.Equal(0.5, p.NCol);
        Assert.Equal(7.5, p.X);
        Assert.Equal(27.5, p.Y);
    }
}
