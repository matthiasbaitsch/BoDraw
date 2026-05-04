using System.Collections;
using Avalonia;

namespace BoDraw;

internal class ShapeCollection : IEnumerable<Shape>
{

    private List<Shape> shapes = [];

    internal void Add(params Shape[] shapes)
    {
        foreach (var s in shapes)
        {
            this.shapes.RemoveAll(x => x == s);
            this.shapes.Add(s);
        }
    }

    internal int Count { get { return this.shapes.Count; } }

    internal void Clear() { this.shapes.Clear(); }

    internal Shape Get(int index) { return this.shapes[index]; }

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
