using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Abstract base class for line-like shapes (e.g. <see cref="Line"/>, <see cref="Polyline"/>).
/// Provides <see cref="Color"/> and <see cref="Thickness"/> properties backed by an Avalonia <see cref="Pen"/>.
/// </summary>
public abstract class LineLikeShape : Shape
{
    public Pen Pen = new Pen(new SolidColorBrush(Colors.Black), lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round);

    /// <summary>The stroke color.</summary>
    public Color Color
    {
        get { return ((SolidColorBrush?)this.Pen.Brush)!.Color; }
        set { ((SolidColorBrush?)this.Pen.Brush)!.Color = value; }
    }

    /// <summary>The opacity of the stroke in the range [0, 1].</summary>
    public double Opacity
    {
        get { return ((SolidColorBrush?)this.Pen.Brush)!.Opacity; }
        set { ((SolidColorBrush?)this.Pen.Brush)!.Opacity = value; }
    }

    /// <summary>The stroke thickness in drawing units.</summary>
    public double Thickness
    {
        get { return this.Pen.Thickness; }
        set { this.Pen.Thickness = value; }
    }

    // TODO doc, demo
    public double[] DashStyle
    {
        set { this.Pen.DashStyle = new DashStyle(value, 0); }
    }

    internal override sealed void Draw(double scale, DrawingContext ctx)
    {
        this.Draw(ctx, Shape.ScalePen(scale, this.Pen)!);
    }

    protected abstract void Draw(DrawingContext ctx, Pen pen);

}
