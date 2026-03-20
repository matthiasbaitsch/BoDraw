using Avalonia.Media;

namespace bodraw;

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