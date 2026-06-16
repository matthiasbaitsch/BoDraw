using System.Runtime.CompilerServices;
using SkiaSharp;

namespace BoDraw.Tests;

static class ImageComparer
{
    private const int ChannelTolerance = 10;
    private const double PixelRatioTolerance = 0.03;

    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.RegisterStreamComparer("png", Compare);
    }

    private static Task<CompareResult> Compare(Stream received, Stream verified, IReadOnlyDictionary<string, object> context)
    {
        using SKBitmap receivedBitmap = SKBitmap.Decode(received);
        using SKBitmap verifiedBitmap = SKBitmap.Decode(verified);

        if (receivedBitmap.Width != verifiedBitmap.Width || receivedBitmap.Height != verifiedBitmap.Height)
        {
            return Task.FromResult(CompareResult.NotEqual(
                $"Size mismatch: {receivedBitmap.Width}x{receivedBitmap.Height} vs {verifiedBitmap.Width}x{verifiedBitmap.Height}"));
        }

        int diffPixels = 0;
        int total = receivedBitmap.Width * receivedBitmap.Height;

        for (int y = 0; y < receivedBitmap.Height; y++)
        {
            for (int x = 0; x < receivedBitmap.Width; x++)
            {
                SKColor r = receivedBitmap.GetPixel(x, y);
                SKColor v = verifiedBitmap.GetPixel(x, y);

                if (Math.Abs(r.Red - v.Red) > ChannelTolerance
                    || Math.Abs(r.Green - v.Green) > ChannelTolerance
                    || Math.Abs(r.Blue - v.Blue) > ChannelTolerance
                    || Math.Abs(r.Alpha - v.Alpha) > ChannelTolerance)
                {
                    diffPixels++;
                }
            }
        }

        double ratio = (double)diffPixels / total;
        if (ratio > PixelRatioTolerance)
        {
            return Task.FromResult(CompareResult.NotEqual(
                $"{ratio:P1} of pixels differ by more than {ChannelTolerance} per channel (tolerance: {PixelRatioTolerance:P0})"));
        }

        return Task.FromResult(CompareResult.Equal);
    }
}
