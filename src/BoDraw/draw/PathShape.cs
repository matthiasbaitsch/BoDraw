using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A shape built from line, curve, and arc segments — either parsed from Avalonia's path mini-language
/// (e.g. "M 0,0 L 10,0 L 10,10 Z") or added programmatically via <see cref="MoveTo"/>, <see cref="LineTo"/>,
/// <see cref="CurveTo"/>, <see cref="QuadTo"/>, <see cref="ArcTo"/>, and <see cref="Close"/>.
/// See <see href="https://docs.avaloniaui.net/docs/graphics-animation/shapes-and-geometries#path-mini-language">
/// Avalonia's path mini-language documentation</see> for the grammar.
/// </summary>
/// <example>
/// One ring of the "G" logo, built programmatically from arcs and lines:
/// <code>
/// PathShape path = new PathShape();
/// path.FillColor = null;
/// path.LineThickness = 10;
/// path.MoveTo(20, 10);
/// path.ArcTo(-20, 10, 20);
/// path.LineTo(-20, -10);
/// path.ArcTo(20, -10, 20);
/// path.LineTo(20, 5);
/// path.LineTo(0, 5);
/// bd.Add(path);
/// </code>
/// See <see href="../../demo/BoDraw.App.Demo/GLogoDemo.cs">GLogoDemo</see> for the full multi-ring version.
/// </example>
public class PathShape : AreaLikeShape
{
    // Each segment carries its own points and its own drawing behavior (as a closure), so appending
    // it to the geometry or transforming its points never has to switch on what kind of segment it is.
    private sealed class Segment(Point[] points, Action<StreamGeometryContext, Point[]> draw)
    {
        private readonly Point[] points = points;
        private readonly Action<StreamGeometryContext, Point[]> draw = draw;

        internal void AppendTo(StreamGeometryContext ctx)
        {
            this.draw(ctx, this.points);
        }

        internal Segment Transform(Matrix t)
        {
            return new Segment(Array.ConvertAll(this.points, p => p.Transform(t)), this.draw);
        }
    }

    /// <summary>A subpath: a start point, its segments, and whether it is closed back to that start point.</summary>
    private sealed class Figure
    {
        internal Point Start;
        internal bool Closed;
        internal readonly List<Segment> Segments = [];
    }

    private List<Figure> figures = [];
    private Figure? current;

    /// <summary>Creates an empty path. Segments are added via <see cref="MoveTo"/>, <see cref="LineTo"/>, etc.</summary>
    public PathShape() { }

    /// <summary>Creates a path from Avalonia path markup syntax.</summary>
    public PathShape(string data)
    {
        foreach (PathFigure figure in PathGeometry.Parse(data).Figures!)
        {
            this.MoveTo(figure.StartPoint.X, figure.StartPoint.Y);
            foreach (PathSegment segment in figure.Segments!)
            {
                this.AddParsedSegment(segment);
            }
            if (figure.IsClosed)
            {
                this.Close();
            }
        }
    }

    // The one place a type switch is unavoidable: Avalonia's parsed PathSegment hierarchy is closed
    // (sealed, foreign), so it cannot be extended with the AppendTo/Transform behavior above. This
    // adapts it into our own segments by replaying it through the public MoveTo/LineTo/... API.
    private void AddParsedSegment(PathSegment segment)
    {
        switch (segment)
        {
            case LineSegment line:
                this.LineTo(line.Point.X, line.Point.Y);
                break;
            case PolyLineSegment polyLine:
                foreach (Point p in polyLine.Points)
                {
                    this.LineTo(p.X, p.Y);
                }
                break;
            case BezierSegment bezier:
                this.CurveTo(bezier.Point1.X, bezier.Point1.Y, bezier.Point2.X, bezier.Point2.Y, bezier.Point3.X, bezier.Point3.Y);
                break;
            case PolyBezierSegment polyBezier:
                for (int i = 0; i + 2 < polyBezier.Points!.Count; i += 3)
                {
                    Point p1 = polyBezier.Points[i];
                    Point p2 = polyBezier.Points[i + 1];
                    Point p3 = polyBezier.Points[i + 2];
                    this.CurveTo(p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y);
                }
                break;
            case QuadraticBezierSegment quad:
                this.QuadTo(quad.Point1.X, quad.Point1.Y, quad.Point2.X, quad.Point2.Y);
                break;
            case ArcSegment arc:
                this.ArcTo(
                    arc.Point.X, arc.Point.Y, arc.Size.Width, arc.Size.Height,
                    arc.RotationAngle, arc.IsLargeArc, arc.SweepDirection == SweepDirection.Clockwise
                );
                break;
        }
    }

    /// <summary>Starts a new subpath at (<paramref name="x"/>, <paramref name="y"/>).</summary>
    public void MoveTo(double x, double y)
    {
        this.current = new Figure { Start = new Point(x, y) };
        this.figures.Add(this.current);
    }

    /// <summary>Appends a straight line segment to (<paramref name="x"/>, <paramref name="y"/>).</summary>
    public void LineTo(double x, double y)
    {
        this.AddSegment([new Point(x, y)], (ctx, p) => ctx.LineTo(p[0]));
    }

    /// <summary>
    /// Appends a cubic Bezier segment with control points (<paramref name="x1"/>, <paramref name="y1"/>) and
    /// (<paramref name="x2"/>, <paramref name="y2"/>), ending at (<paramref name="x"/>, <paramref name="y"/>).
    /// </summary>
    public void CurveTo(double x1, double y1, double x2, double y2, double x, double y)
    {
        this.AddSegment(
            [new Point(x1, y1), new Point(x2, y2), new Point(x, y)],
            (ctx, p) => ctx.CubicBezierTo(p[0], p[1], p[2])
        );
    }

    /// <summary>
    /// Appends a quadratic Bezier segment with control point (<paramref name="x1"/>, <paramref name="y1"/>),
    /// ending at (<paramref name="x"/>, <paramref name="y"/>).
    /// </summary>
    public void QuadTo(double x1, double y1, double x, double y)
    {
        this.AddSegment([new Point(x1, y1), new Point(x, y)], (ctx, p) => ctx.QuadraticBezierTo(p[0], p[1]));
    }

    /// <summary>Appends a circular arc segment of radius <paramref name="r"/>, ending at (<paramref name="x"/>, <paramref name="y"/>).</summary>
    public void ArcTo(double x, double y, double r)
    {
        this.ArcTo(x, y, r, r);
    }

    /// <summary>
    /// Appends an elliptical arc segment ending at (<paramref name="x"/>, <paramref name="y"/>), with radii
    /// <paramref name="radiusX"/>/<paramref name="radiusY"/> and optional rotation, large-arc, and sweep-direction flags.
    /// </summary>
    public void ArcTo(
        double x, double y, double radiusX, double radiusY,
        double rotationAngle = 0, bool isLargeArc = false, bool clockwise = true
    )
    {
        Size size = new Size(radiusX, radiusY);
        SweepDirection sweep = clockwise ? SweepDirection.Clockwise : SweepDirection.CounterClockwise;
        this.AddSegment([new Point(x, y)], (ctx, p) => ctx.ArcTo(p[0], size, rotationAngle, isLargeArc, sweep));
    }

    /// <summary>Closes the current subpath, connecting it back to its start point.</summary>
    public void Close()
    {
        this.RequireCurrentFigure().Closed = true;
    }

    private void AddSegment(Point[] points, Action<StreamGeometryContext, Point[]> draw)
    {
        this.RequireCurrentFigure().Segments.Add(new Segment(points, draw));
    }

    private Figure RequireCurrentFigure()
    {
        if (this.current == null)
        {
            throw new InvalidOperationException($"Call {nameof(this.MoveTo)} before adding segments.");
        }
        return this.current;
    }

    public override void ApplyTransform(Matrix t)
    {
        foreach (Figure figure in this.figures)
        {
            figure.Start = figure.Start.Transform(t);
            for (int i = 0; i < figure.Segments.Count; i++)
            {
                figure.Segments[i] = figure.Segments[i].Transform(t);
            }
        }
    }

    protected internal override Shape DeepClone()
    {
        var copy = (PathShape)base.DeepClone();
        copy.figures = [];
        copy.current = null;

        foreach (Figure figure in this.figures)
        {
            var newFigure = new Figure { Start = figure.Start, Closed = figure.Closed };
            newFigure.Segments.AddRange(figure.Segments);
            copy.figures.Add(newFigure);
            if (ReferenceEquals(figure, this.current))
            {
                copy.current = newFigure;
            }
        }

        return copy;
    }

    internal override Geometry Geometry
    {
        get
        {
            var geo = new StreamGeometry();
            using (StreamGeometryContext ctx = geo.Open())
            {
                foreach (Figure figure in this.figures)
                {
                    ctx.BeginFigure(figure.Start, true);
                    foreach (Segment segment in figure.Segments)
                    {
                        segment.AppendTo(ctx);
                    }
                    ctx.EndFigure(figure.Closed);
                }
            }
            return geo;
        }
    }
}
