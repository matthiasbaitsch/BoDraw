using Avalonia.Media;

namespace bodraw;

/// <summary>
/// Abstract base class for filled area shapes (e.g. <see cref="Rectangle"/>, <see cref="Ellipse"/>).
/// Provides <see cref="FillColor"/>, <see cref="LineColor"/>, and <see cref="LineThickness"/> properties
/// backed by an Avalonia <see cref="Brush"/> and <see cref="Pen"/>.
/// </summary>
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
                this.Brush = new SolidColorBrush((Color)value);
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
                if (this.Pen == null)
                {
                    this.Pen = new Pen(
                        new SolidColorBrush((Color)value),
                        lineCap: PenLineCap.Round,
                        lineJoin: PenLineJoin.Round
                    );
                }
                else
                {
                    ((SolidColorBrush?)this.Pen!.Brush)!.Color = (Color)value;
                }
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