using Avalonia.Media;

using bodraw;

public abstract class AreaLikeShape : Shape
{
    public Brush? Brush = new SolidColorBrush(bodraw.Colors.LightGray);
    public Pen? Pen = new Pen(new SolidColorBrush(bodraw.Colors.Black), lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round);

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

    public override sealed void Draw(double a, DrawingContext ctx)
    {
        this.Draw(ctx, this.Brush, Shape.ScalePen(a, this.Pen));
    }

    protected abstract void Draw(DrawingContext ctx, Brush? brush, Pen? pen);

}