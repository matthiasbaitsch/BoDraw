using System.Collections;
using Avalonia;

namespace BoDraw;

/// <summary>
/// An ordered collection of <see cref="Shape"/> objects. Adding a shape that is already
/// present moves it to the end rather than creating a duplicate.
/// </summary>
internal class ShapeCollection : IEnumerable<Shape>
{

    private List<Shape> shapes = [];

    /// <summary>Adds <paramref name="shapes"/>, moving any duplicates to the end.</summary>
    internal void Add(params Shape[] shapes)
    {
        foreach (var s in shapes)
        {
            this.shapes.RemoveAll(x => x == s);
            this.shapes.Add(s);
        }
    }

    /// <summary>The number of shapes in the collection.</summary>
    internal int Count { get { return this.shapes.Count; } }

    /// <summary>Removes all shapes from the collection.</summary>
    internal void Clear() { this.shapes.Clear(); }

    /// <summary>Returns the shape at <paramref name="index"/>.</summary>
    internal Shape Get(int index) { return this.shapes[index]; }

    /// <summary>The axis-aligned bounding box that encloses all shapes.</summary>
    internal Rect Bounds
    {
        get
        {
            Rect b = new Rect(0, 0, 0, 0);
            foreach (Shape s in this.shapes)
            {
                b = b.Union(s.Bounds);
            }
            return b;
        }
    }

    public IEnumerator<Shape> GetEnumerator() { return this.shapes.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

}
