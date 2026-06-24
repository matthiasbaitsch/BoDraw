using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace BoDraw;

/// <summary>Extension methods for Avalonia <see cref="Bitmap"/>.</summary>
internal static class ImageExtensions
{

    /// <summary>Reads the color of the pixel at column <paramref name="x"/>, row <paramref name="y"/>.</summary>
    internal static Color ColorAt(this Bitmap bitmap, int x, int y)
    {
        byte[] buffer = new byte[4];

        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                bitmap.CopyPixels(new PixelRect(x, y, 1, 1), (nint)ptr, 4, 4);
            }
        }

        return Color.FromArgb(
            a: buffer[0],
            r: buffer[1],
            g: buffer[2],
            b: buffer[3]);
    }
}