using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Maps scalar values to colors by interpolating between a sequence of color stops.
/// </summary>
public class ColorMap
{
    private readonly Color[] stops;

    /// <summary>The minimum value of the mapped range.</summary>
    public double Min { get; }

    /// <summary>The maximum value of the mapped range.</summary>
    public double Max { get; }

    /// <summary>
    /// Creates a color map with the given color stops and value range.
    /// </summary>
    /// <param name="stops">Two or more colors spanning the range from Min to Max.</param>
    /// <param name="min">Value that maps to the first stop.</param>
    /// <param name="max">Value that maps to the last stop.</param>
    public ColorMap(Color[] stops, double min = 0.0, double max = 1.0)
    {
        this.stops = stops;
        this.Min = min;
        this.Max = max;
    }

    /// <summary>
    /// Returns a new color map with the same stops but a different value range.
    /// </summary>
    public ColorMap WithRange(double min, double max)
    {
        return new ColorMap(this.stops, min, max);
    }

    /// <summary>
    /// Maps a value in [Min, Max] to a color. Values outside the range are clamped.
    /// </summary>
    public Color Map(double value)
    {
        double t = (value - this.Min) / (this.Max - this.Min);
        t = Math.Clamp(t, 0.0, 1.0);

        double scaled = t * (this.stops.Length - 1);
        int i = (int)scaled;
        double f = scaled - i;

        if (i >= this.stops.Length - 1)
        {
            return this.stops[^1];
        }

        return Lerp(this.stops[i], this.stops[i + 1], f);
    }

    private static Color Lerp(Color a, Color b, double t)
    {
        return Color.FromArgb(
            (byte)(a.A + (b.A - a.A) * t),
            (byte)(a.R + (b.R - a.R) * t),
            (byte)(a.G + (b.G - a.G) * t),
            (byte)(a.B + (b.B - a.B) * t)
        );
    }

    /// <summary>Black to white.</summary>
    public static ColorMap Grayscale { get; } = new ColorMap(new[]
    {
        Color.FromRgb(0, 0, 0),
        Color.FromRgb(255, 255, 255)
    });

    /// <summary>Black → red → yellow → white.</summary>
    public static ColorMap Hot { get; } = new ColorMap(new[]
    {
        Color.FromRgb(0, 0, 0),
        Color.FromRgb(255, 0, 0),
        Color.FromRgb(255, 255, 0),
        Color.FromRgb(255, 255, 255)
    });

    /// <summary>Cyan to magenta.</summary>
    public static ColorMap Cool { get; } = new ColorMap(new[]
    {
        Color.FromRgb(0, 255, 255),
        Color.FromRgb(255, 0, 255)
    });

    /// <summary>Blue → cyan → green → yellow → red.</summary>
    public static ColorMap Jet { get; } = new ColorMap(new[]
    {
        Color.FromRgb(0, 0, 127),
        Color.FromRgb(0, 0, 255),
        Color.FromRgb(0, 127, 255),
        Color.FromRgb(0, 255, 255),
        Color.FromRgb(127, 255, 127),
        Color.FromRgb(255, 255, 0),
        Color.FromRgb(255, 127, 0),
        Color.FromRgb(255, 0, 0),
        Color.FromRgb(127, 0, 0)
    });

    /// <summary>Perceptually uniform purple → blue → green → yellow.</summary>
    public static ColorMap Viridis { get; } = new ColorMap(new[]
    {
        Color.FromRgb(68, 1, 84),
        Color.FromRgb(72, 40, 120),
        Color.FromRgb(62, 83, 160),
        Color.FromRgb(49, 104, 142),
        Color.FromRgb(38, 130, 142),
        Color.FromRgb(31, 158, 137),
        Color.FromRgb(53, 183, 121),
        Color.FromRgb(110, 206, 88),
        Color.FromRgb(181, 222, 43),
        Color.FromRgb(253, 231, 37)
    });

    /// <summary>Perceptually uniform purple → pink → orange → yellow.</summary>
    public static ColorMap Plasma { get; } = new ColorMap(new[]
    {
        Color.FromRgb(13, 8, 135),
        Color.FromRgb(84, 2, 163),
        Color.FromRgb(139, 10, 165),
        Color.FromRgb(185, 50, 137),
        Color.FromRgb(219, 92, 104),
        Color.FromRgb(244, 136, 73),
        Color.FromRgb(254, 188, 43),
        Color.FromRgb(240, 249, 33)
    });

    /// <summary>Perceptually uniform black → purple → orange → yellow.</summary>
    public static ColorMap Inferno { get; } = new ColorMap(new[]
    {
        Color.FromRgb(0, 0, 4),
        Color.FromRgb(40, 11, 84),
        Color.FromRgb(101, 21, 110),
        Color.FromRgb(159, 42, 99),
        Color.FromRgb(212, 72, 66),
        Color.FromRgb(245, 125, 21),
        Color.FromRgb(250, 193, 39),
        Color.FromRgb(252, 255, 164)
    });

    /// <summary>Diverging red → white → blue.</summary>
    public static ColorMap RdBu { get; } = new ColorMap(new[]
    {
        Color.FromRgb(178, 24, 43),
        Color.FromRgb(214, 96, 77),
        Color.FromRgb(244, 165, 130),
        Color.FromRgb(253, 219, 199),
        Color.FromRgb(255, 255, 255),
        Color.FromRgb(209, 229, 240),
        Color.FromRgb(146, 197, 222),
        Color.FromRgb(67, 147, 195),
        Color.FromRgb(33, 102, 172)
    });
}
