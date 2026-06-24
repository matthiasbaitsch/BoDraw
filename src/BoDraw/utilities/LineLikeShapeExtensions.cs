using Avalonia.Media;

namespace BoDraw;

public static class LineLikeShapeExtensions
{
    /// <summary>Sets <see cref="LineLikeShape.Color"/> to <paramref name="color"/> and returns <paramref name="shape"/>.</summary>
    public static T WithColor<T>(this T shape, Color color) where T : LineLikeShape
    {
        shape.Color = color;
        return shape;
    }

    /// <summary>Sets <see cref="LineLikeShape.Thickness"/> to <paramref name="thickness"/> and returns <paramref name="shape"/>.</summary>
    public static T WithThickness<T>(this T shape, double thickness) where T : LineLikeShape
    {
        shape.Thickness = thickness;
        return shape;
    }

    /// <summary>Sets <see cref="LineLikeShape.DashStyle"/> to <paramref name="style"/> and returns <paramref name="shape"/>.</summary>
    public static T WithDashStyle<T>(this T shape, params double[] style) where T : LineLikeShape
    {
        shape.DashStyle = style;
        return shape;
    }
}
