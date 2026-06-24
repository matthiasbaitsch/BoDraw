using Avalonia;

namespace BoDraw;


/// <summary>Extension and factory methods for Avalonia <see cref="Matrix"/>.</summary>
internal static class MatrixExtensions
{
    /// <summary>Creates a scale matrix by (<paramref name="sx"/>, <paramref name="sy"/>) around <paramref name="center"/>.</summary>
    public static Matrix CreateScale(double sx, double sy, Point center)
    {
        var v = new Vector(center.X, center.Y);
        return
            Matrix.CreateTranslation(-v).Append(
            Matrix.CreateScale(sx, sy)).Append(
            Matrix.CreateTranslation(v));
    }
}