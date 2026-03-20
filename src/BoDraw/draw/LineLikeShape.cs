using Avalonia;
using Avalonia.Media;

using bodraw;

public abstract class LineLikeShape : Shape
{
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

    public override sealed void Draw(double a, DrawingContext ctx)
    {
        this.Draw(ctx, Shape.ScalePen(a, this.Pen)!);
    }

    protected abstract void Draw(DrawingContext ctx, Pen pen);

}