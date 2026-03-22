using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Abstract base class for line-like shapes (e.g. <see cref="Line"/>, <see cref="Polyline"/>).
/// Provides <see cref="Color"/> and <see cref="Thickness"/> properties backed by an Avalonia <see cref="Pen"/>.
/// </summary>
public abstract class LineLikeShape : Shape
{
    public Pen Pen = new Pen(new SolidColorBrush(Colors.Black), lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round);

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

    internal override sealed void Draw(double scale, DrawingContext ctx)
    {
        this.Draw(ctx, Shape.ScalePen(scale, this.Pen)!);
    }

    protected abstract void Draw(DrawingContext ctx, Pen pen);

}