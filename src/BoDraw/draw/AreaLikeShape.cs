using Avalonia.Media;

namespace bodraw;

public abstract class AreaLikeShape : Shape
{
    public Brush? Brush = new SolidColorBrush(Colors.LightGray);
    public Pen? Pen = new Pen(new SolidColorBrush(Colors.Black), lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round);

    public Color? FillColor
    {
        get
        {
            if (this.Brush == null)
            {
                return null;
            }
            return ((SolidColorBrush)this.Brush).Color;
        }

        set
        {
            if (value == null)
            {
                this.Brush = null;
            }
            else
            {
                ((SolidColorBrush)this.Brush!).Color = (Color)value;
            }
        }
    }

    public Color? LineColor
    {
        get
        {
            if (this.Pen == null)
            {
                return null;
            }
            return ((SolidColorBrush?)this.Pen.Brush)!.Color;
        }
        set
        {
            if (value == null)
            {
                this.Pen = null;
            }
            else
            {
                ((SolidColorBrush?)this.Pen!.Brush)!.Color = (Color)value;
            }
        }
    }

    public double LineThickness
    {
        get { return this.Pen!.Thickness; }
        set { this.Pen!.Thickness = value; }
    }

    internal override sealed void Draw(double scale, DrawingContext ctx)
    {
        this.Draw(ctx, this.Brush, Shape.ScalePen(scale, this.Pen));
    }

    protected abstract void Draw(DrawingContext ctx, Brush? brush, Pen? pen);

}