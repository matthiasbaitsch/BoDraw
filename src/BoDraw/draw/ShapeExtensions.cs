using Avalonia;

namespace BoDraw;

public static class ShapeExtensions
{
    public static T Move<T>(this T shape, double dx, double dy) where T : Shape
    {
        shape.ApplyTransform(Matrix.CreateTranslation(dx, dy));
        return shape;
    }

    public static T Scale<T>(this T shape, double sx, double sy) where T : Shape
    {
        return shape.Scale(sx, sy, shape.TransformationCenter);
    }

    public static T Scale<T>(this T shape, double sx, double sy, Point center) where T : Shape
    {
        shape.ApplyTransform(MatrixExtensions.CreateScale(sx, sy, center));
        return shape;
    }

    public static T Scale<T>(this T shape, double factor) where T : Shape
    {
        return shape.Scale(factor, factor, shape.TransformationCenter);
    }

    public static T Scale<T>(this T shape, double factor, Point center) where T : Shape
    {
        return shape.Scale(factor, factor, center);
    }

    public static T Rotate<T>(this T shape, double angle) where T : Shape
    {
        return shape.Rotate(angle, shape.TransformationCenter);
    }

    public static T Rotate<T>(this T shape, double angle, Point center) where T : Shape
    {
        shape.ApplyTransform(Matrix.CreateRotation(angle * Math.PI / 180.0, center));
        return shape;
    }

    public static T FitInto<T>(this T shape, Rectangle target, bool keepAspect = false) where T : Shape
    {
        shape.ApplyTransform(shape.Bounds.TransformInto(target.Bounds, keepAspect));
        return shape;
    }

    public static T Copy<T>(this T shape) where T : Shape
    {
        return (T)shape.DeepClone();
    }

    public static T Copy<T>(this T shape, double dx, double dy) where T : Shape
    {
        var copy = (T)shape.DeepClone();
        copy.Move(dx, dy);
        return copy;
    }
}
