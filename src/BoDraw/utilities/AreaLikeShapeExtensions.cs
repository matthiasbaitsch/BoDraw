using Avalonia.Media;

namespace BoDraw;

public static class AreaLikeShapeExtensions
{
    /// <summary>Sets <see cref="AreaLikeShape.FillColor"/> to <paramref name="color"/> and returns <paramref name="shape"/>.</summary>
    public static T WithFillColor<T>(this T shape, Color color) where T : AreaLikeShape
    {
        shape.FillColor = color;
        return shape;
    }

    /// <summary>Sets <see cref="AreaLikeShape.FillOpacity"/> to <paramref name="opacity"/> and returns <paramref name="shape"/>.</summary>
    public static T WithFillOpacity<T>(this T shape, double opacity) where T : AreaLikeShape
    {
        shape.FillOpacity = opacity;
        return shape;
    }

    /// <summary>Sets <see cref="AreaLikeShape.LineColor"/> to <paramref name="color"/> and returns <paramref name="shape"/>.</summary>
    public static T WithLineColor<T>(this T shape, Color color) where T : AreaLikeShape
    {
        shape.LineColor = color;
        return shape;
    }

    /// <summary>Sets <see cref="AreaLikeShape.LineThickness"/> to <paramref name="thickness"/> and returns <paramref name="shape"/>.</summary>
    public static T WithLineThickness<T>(this T shape, double thickness) where T : AreaLikeShape
    {
        shape.LineThickness = thickness;
        return shape;
    }
}
