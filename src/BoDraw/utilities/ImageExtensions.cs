using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace BoDraw;

internal static class ImageExtensions
{

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