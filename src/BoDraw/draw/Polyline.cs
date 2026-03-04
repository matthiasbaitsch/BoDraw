using Avalonia;
using Avalonia.Media;

namespace bodraw;

public class Polyline : Shape
{
    private readonly List<Point> points = [];

    public Pen Pen = new Pen(new SolidColorBrush(Avalonia.Media.Colors.Black), lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round);

    public Color Color
    {
        get
        {
            return ((SolidColorBrush?)this.Pen.Brush)!.Color;
        }
        set
        {
            ((SolidColorBrush?)this.Pen.Brush)!.Color = value;
        }
    }

    public double Thickness
    {
        get
        {
            return this.Pen.Thickness;
        }
        set
        {
            this.Pen.Thickness = value;
        }
    }

    public override Rect Bounds
    {
        get
        {
            double xmin = this.points[0].X;
            double xmax = xmin;
            double ymin = this.points[0].Y;
            double ymax = ymin;

            foreach (Point p in this.points)
            {

                xmin = Math.Min(xmin, p.X);
                xmax = Math.Max(xmax, p.X);
                ymin = Math.Min(ymin, p.Y);
                ymax = Math.Max(ymax, p.Y);
            }

            return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
        }
    }

    public void AddPoint(double x, double y)
    {
        this.points.Add(new Point((float)x, (float)y));
    }

    public override void Draw(DrawingContext ctx)
    {
        if (this.points.Count < 2) { return; }
        var geo = new StreamGeometry();
        using (var sgc = geo.Open())
        {
            sgc.BeginFigure(this.points[0], false);
            for (int i = 1; i < this.points.Count; i++)
                sgc.LineTo(this.points[i]);
            sgc.EndFigure(false);
        }
        ctx.DrawGeometry(null, this.Pen, geo);
    }
}

