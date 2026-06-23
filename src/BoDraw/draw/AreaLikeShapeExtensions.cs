using Avalonia.Media;

namespace BoDraw;

public static class AreaLikeShapeExtensions
{
    public static T WithFillColor<T>(this T shape, Color color) where T : AreaLikeShape
    {
        shape.FillColor = color;
        return shape;
    }

    public static T WithFillOpacity<T>(this T shape, double opacity) where T : AreaLikeShape
    {
        shape.FillOpacity = opacity;
        return shape;
    }

    public static T WithLineColor<T>(this T shape, Color color) where T : AreaLikeShape
    {
        shape.LineColor = color;
        return shape;
    }

    public static T WithLineThickness<T>(this T shape, double thickness) where T : AreaLikeShape
    {
        shape.LineThickness = thickness;
        return shape;
    }
}
