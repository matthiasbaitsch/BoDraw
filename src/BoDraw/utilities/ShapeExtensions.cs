using Avalonia;

namespace BoDraw;

public static class ShapeExtensions
{
    /// <summary>Translates <paramref name="shape"/> by (<paramref name="dx"/>, <paramref name="dy"/>) and returns it.</summary>
    public static T Move<T>(this T shape, double dx, double dy) where T : Shape
    {
        shape.ApplyTransform(Matrix.CreateTranslation(dx, dy));
        return shape;
    }

    /// <summary>Scales <paramref name="shape"/> uniformly by <paramref name="factor"/> around its center and returns it.</summary>
    public static T Scale<T>(this T shape, double factor) where T : Shape
    {
        return shape.Scale(factor, factor, shape.TransformationCenter);
    }

    /// <summary>Scales <paramref name="shape"/> by (<paramref name="sx"/>, <paramref name="sy"/>) around its center and returns it.</summary>
    public static T Scale<T>(this T shape, double sx, double sy) where T : Shape
    {
        return shape.Scale(sx, sy, shape.TransformationCenter);
    }

    /// <summary>Scales <paramref name="shape"/> by (<paramref name="sx"/>, <paramref name="sy"/>) around (<paramref name="cx"/>, <paramref name="cy"/>) and returns it.</summary>
    public static T Scale<T>(this T shape, double sx, double sy, double cx, double cy) where T : Shape
    {
        return shape.Scale(sx, sy, new Point(cx, cy));
    }

    /// <summary>Scales <paramref name="shape"/> uniformly by <paramref name="factor"/> around <paramref name="center"/> and returns it.</summary>
    public static T Scale<T>(this T shape, double factor, Point center) where T : Shape
    {
        return shape.Scale(factor, factor, center);
    }

    /// <summary>Scales <paramref name="shape"/> by (<paramref name="sx"/>, <paramref name="sy"/>) around <paramref name="center"/> and returns it.</summary>
    public static T Scale<T>(this T shape, double sx, double sy, Point center) where T : Shape
    {
        shape.ApplyTransform(MatrixExtensions.CreateScale(sx, sy, center));
        return shape;
    }

    /// <summary>Rotates <paramref name="shape"/> by <paramref name="angle"/> degrees around its center and returns it.</summary>
    public static T Rotate<T>(this T shape, double angle) where T : Shape
    {
        return shape.Rotate(angle, shape.TransformationCenter);
    }

    /// <summary>Rotates <paramref name="shape"/> by <paramref name="angle"/> degrees around (<paramref name="cx"/>, <paramref name="cy"/>) and returns it.</summary>
    public static T Rotate<T>(this T shape, double angle, double cx, double cy) where T : Shape
    {
        return shape.Rotate(angle, new Point(cx, cy));
    }

    /// <summary>Rotates <paramref name="shape"/> by <paramref name="angle"/> degrees around <paramref name="center"/> and returns it.</summary>
    public static T Rotate<T>(this T shape, double angle, Point center) where T : Shape
    {
        shape.ApplyTransform(Matrix.CreateRotation(angle * Math.PI / 180.0, center));
        return shape;
    }

    /// <summary>Transforms <paramref name="shape"/> to fit inside <paramref name="target"/>, optionally preserving aspect ratio, and returns it.</summary>
    public static T FitInto<T>(this T shape, Rectangle target, bool keepAspect = false) where T : Shape
    {
        shape.ApplyTransform(shape.Bounds.TransformInto(target.Bounds, keepAspect));
        return shape;
    }

    /// <summary>Returns a deep copy of <paramref name="shape"/>.</summary>
    public static T Copy<T>(this T shape) where T : Shape
    {
        return (T)shape.DeepClone();
    }

    /// <summary>Returns a deep copy of <paramref name="shape"/> translated by (<paramref name="dx"/>, <paramref name="dy"/>).</summary>
    public static T Copy<T>(this T shape, double dx, double dy) where T : Shape
    {
        var copy = (T)shape.DeepClone();
        copy.Move(dx, dy);
        return copy;
    }
}
