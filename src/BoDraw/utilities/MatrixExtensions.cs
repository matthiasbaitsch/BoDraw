using Avalonia;

namespace BoDraw;


internal static class MatrixExtensions
{
    public static Matrix CreateScale(double sx, double sy, Point center)
    {
        return
            Matrix.CreateTranslation(-center).Append(
            Matrix.CreateScale(sx, sy)).Append(
            Matrix.CreateTranslation(center));
    }
}