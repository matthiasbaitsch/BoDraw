using Avalonia.Media;

namespace BoDraw;

public static class LineLikeShapeExtensions
{
    public static T WithColor<T>(this T shape, Color color) where T : LineLikeShape
    {
        shape.Color = color;
        return shape;
    }

    public static T WithThickness<T>(this T shape, double thickness) where T : LineLikeShape
    {
        shape.Thickness = thickness;
        return shape;
    }
}
