namespace BoDraw;

public static class ShapeExtensions
{
    /// <summary>
    /// Returns a copy of the shape moved by the given offset.
    /// </summary>
    /// <param name="shape">The shape to copy.</param>
    /// <param name="dx">Horizontal offset.</param>
    /// <param name="dy">Vertical offset.</param>
    public static T Copy<T>(this T shape, double dx, double dy) where T : Shape
    {
        var copy = (T)shape.DeepClone();
        copy.Move(dx, dy);
        return copy;
    }
}
