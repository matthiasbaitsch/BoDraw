using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Produces dimension lines for a drawing. Stores style settings such as font size,
/// scaling factor, and number format, and renders annotated tick lines along one or
/// more chains of measured points.
/// </summary>
/// <example>
/// <code>
/// using BoDraw;
///
/// Polygon p = new Polygon(0,0, 3,0, 5,1, 6,1, 6,6, 0,6);
///
/// Dimensioning dim = new Dimensioning(0.5);
/// dim.ScalingFactor = 100;
/// dim.Format = "0.##cm";
///
/// dim.Start(0, -1.5);
/// dim.HStep(6);
/// dim.StartNext();
/// dim.HStep(3, 5, 6);
///
/// dim.Start(7.5, 0);
/// dim.VStep(6);
/// dim.StartNext();
/// dim.VStep(1, 6);
///
/// BoDrawApp bd = new BoDrawApp();
/// bd.Add(p, dim);
/// bd.Show();
/// </code>
/// </example>
public class Dimensioning : LineLikeShape
{

    private const double D1 = 0.3;
    private const double D2 = 0.1;
    private const double TT = 0.95;
    private const double TF = 0.375;

    private struct CSys
    {
        internal CSys(Point p1, Point p2)
        {
            this.E1 = ((Vector)(p2 - p1)).Normalize();
            this.E2 = new Vector(-this.E1.Y, this.E1.X); ;
        }

        internal readonly double Angle
        {
            get
            {
                return 180 / Math.PI * Math.Atan2(this.E1.Y, this.E1.X);
            }
        }

        internal Vector E1;
        internal Vector E2;
    }

    /// <summary>Format string used to render the measured value as a label (e.g. <c>"0.##"</c>).</summary>
    public String Format { get; set; } = "0.##";

    /// <summary>Multiplied with the raw drawing-unit distance before formatting the label.
    /// Use this to convert drawing units to real-world units (e.g. metres).</summary>
    public double ScalingFactor { get; set; } = 1.0;

    /// <summary>Height of dimensioning in drawing units.</summary>
    public double Height { get; set; }

    /// <summary>Stroke width of the dimension lines and tick marks.</summary>
    public double LineThickness { get; set; } = 1.0;

    /// <summary>Typeface used to render the dimension labels.</summary>
    public Typeface Typeface { get; set; } = new Typeface("Arial");

    private List<List<Point>> points = [];

    /// <summary>The bounding box enclosing all dimension chains, padded by half the tick height.</summary>
    public override Rect Bounds
    {
        get
        {
            double xmin = Double.PositiveInfinity;
            double xmax = Double.NegativeInfinity;
            double ymin = Double.PositiveInfinity;
            double ymax = Double.NegativeInfinity;

            foreach (var pts in this.points)
            {
                foreach (var p in pts)
                {
                    xmin = Math.Min(xmin, p.X);
                    xmax = Math.Max(xmax, p.X);
                    ymin = Math.Min(ymin, p.Y);
                    ymax = Math.Max(ymax, p.Y);
                }
            }

            return new Rect(
                xmin - this.Height / 2,
                ymin - this.Height / 2,
                xmax - xmin + this.Height,
                ymax - ymin + this.Height
            );
        }
    }

    /// <summary>Creates a new <see cref="Dimensioning"/> with the given <paramref name="height"/>
    /// as basis for tick height and label size.</summary>
    public Dimensioning(double height)
    {
        this.Height = height;
    }

    /// <summary>Starts a new dimension chain at (<paramref name="x"/>, <paramref name="y"/>).</summary>
    public void Start(double x, double y)
    {
        this.Start(new Point(x, y));
    }

    /// <summary>Starts a new dimension chain at point <paramref name="p"/>.</summary>
    public void Start(Point p)
    {
        this.points.Add([p]);
    }

    /// <summary>Starts a new dimension chain offset by one <see cref="Height"/> perpendicular
    /// to the last chain, beginning at its first point.</summary>
    public void StartNext()
    {
        var last = this.points.Last();
        var cs = new CSys(last[0], last[1]);
        this.Start(last[0] + this.Height * cs.E2);
    }

    /// <summary>Appends one or more horizontal steps to the current chain, each keeping the
    /// Y coordinate of the previous point and moving to the next value in <paramref name="xs"/>.</summary>
    public void HStep(params double[] xs)
    {
        foreach (var x in xs)
        {
            this.Step(this.points.Last().Last().WithX(x));
        }
    }

    /// <summary>Appends one or more vertical steps to the current chain, each keeping the
    /// X coordinate of the previous point and moving to the next value in <paramref name="ys"/>.</summary>
    public void VStep(params double[] ys)
    {
        foreach (var y in ys)
        {
            this.Step(this.points.Last().Last().WithY(y));
        }
    }

    /// <summary>Appends <paramref name="p"/> to the current dimension chain.</summary>
    private void Step(Point p)
    {
        this.points.Last().Add(p);
    }

    protected override void Draw(DrawingContext ctx, Pen pen)
    {

        var d1 = D1 * this.Height;
        var d2 = D2 * this.Height;

        foreach (var pts in this.points)
        {
            var p1 = pts.First();
            var p2 = pts.Last();
            var csys = new CSys(p1, p2);
            var vl = 0.5 * this.Height * csys.E2;
            var dl = d2 * (csys.E1 + csys.E2);

            // Baseline
            ctx.DrawLine(pen, p1 - d1 * csys.E1, p2 + d1 * csys.E1);

            // Loop over points
            var lastP = pts.First();
            foreach (var p in pts)
            {
                // Lines
                ctx.DrawLine(pen, p - vl, p + vl);
                ctx.DrawLine(pen, p - dl, p + dl);

                // Text if not first point
                if (p != pts.First())
                {
                    var anchor = 0.5 * (lastP + p) + TT * vl;
                    var scaledDistance = this.ScalingFactor * ((Vector)(p - lastP)).Length;
                    var text = new Text(
                        scaledDistance.ToString(this.Format),
                        anchor.X, anchor.Y,
                        TF * this.Height
                    );

                    text.Angle = csys.Angle;
                    text.HJust = 0.5;
                    text.VJust = 1;
                    text.Draw(1, ctx);

                    // Advance
                    lastP = p;
                }
            }
        }
    }

    /// <summary>Not implemented.</summary>
    public override void Move(double dx, double dy)
    {
        throw new NotImplementedException();
    }

    /// <summary>Not implemented.</summary>
    public override void Scale(double factor)
    {
        throw new NotImplementedException();
    }
}