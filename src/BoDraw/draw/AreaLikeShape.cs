using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Abstract base class for filled area shapes (e.g. <see cref="Rectangle"/>, <see cref="Ellipse"/>).
/// Provides <see cref="FillColor"/>, <see cref="LineColor"/>, and <see cref="LineThickness"/> properties
/// backed by an Avalonia <see cref="Brush"/> and <see cref="Pen"/>.
/// </summary>
public abstract class AreaLikeShape : Shape
{
    public Brush? Brush = new SolidColorBrush(Colors.LightGray);
    public Pen? Pen = new Pen(new SolidColorBrush(Colors.Black), lineCap: PenLineCap.Round, lineJoin: PenLineJoin.Round);

    /// <summary>The fill color. Set to null to render without a fill.</summary>
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
                var oldBrush = this.Brush;
                this.Brush = new SolidColorBrush((Color)value);
                if (oldBrush != null)
                {
                    this.Brush.Opacity = oldBrush.Opacity;
                }
            }
        }
    }

    /// <summary>The opacity of the fill in the range [0, 1]. Returns NaN when there is no fill brush.</summary>
    public double FillOpacity
    {
        get
        {
            if (this.Brush == null)
            {
                return double.NaN;
            }
            return this.Brush.Opacity;
        }

        set
        {
            if (this.Brush != null)
            {
                this.Brush.Opacity = value;
            }
        }
    }

    /// <summary>The stroke color. Set to null to render without an outline.</summary>
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

    /// <summary>The stroke thickness in drawing units.</summary>
    public double LineThickness
    {
        get { return this.Pen!.Thickness; }
        set { this.Pen!.Thickness = value; }
    }

    /// <summary>The opacity of the stroke in the range [0, 1].</summary>
    public double LineOpacity
    {
        get { return ((SolidColorBrush?)this.Pen!.Brush)!.Opacity; }
        set { ((SolidColorBrush?)this.Pen!.Brush)!.Opacity = value; }
    }

    /// <summary>The Avalonia geometry that defines the shape's outline.</summary>
    internal abstract Geometry Geometry { get; }

    public override Rect Bounds
    {
        get { return this.Geometry.Bounds; }
    }

    internal override sealed void Draw(double scale, DrawingContext ctx)
    {
        ctx.DrawGeometry(this.Brush, Shape.ScalePen(scale, this.Pen), this.Geometry);
    }

}