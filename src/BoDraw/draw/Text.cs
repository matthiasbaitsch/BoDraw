using System.Globalization;
using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// A text label placed at a given position. Supports horizontal and vertical justification
/// via <see cref="HJust"/> and <see cref="VJust"/>.
/// </summary>
public class Text : SimpleShape
{
    /// <summary>The text string to display.</summary>
    public string Content { get; set; }

    /// <summary>The anchor point of the text in drawing coordinates.</summary>
    public Point Position { get; set; }

    /// <summary>The X coordinate of the anchor point.</summary>
    public double X { get { return this.Position.X; } }

    /// <summary>The Y coordinate of the anchor point.</summary>
    public double Y { get { return this.Position.Y; } }

    /// <summary>The font size.</summary>
    public double FontSize { get; set; }

    /// <summary>Horizontal justification: 0 = left, 0.5 = center, 1 = right.</summary>
    public double HJust { get; set; } = 0;

    /// <summary>Vertical justification: 0 = bottom, 0.5 = middle, 1 = top.</summary>
    public double VJust { get; set; } = 0;

    /// <summary>Rotation angle in degrees, measured counter-clockwise from the positive X-axis.</summary>
    public double Angle { get; set; } = 0;

    /// <summary>The typeface used to render the text.</summary>
    public Typeface Typeface { get; set; } = new Typeface("Arial");

    /// <summary>The color of the text.</summary>
    public Color Color { get; set; } = Colors.Black;

    /// <summary>Creates text at position (<paramref name="x"/>, <paramref name="y"/>) 
    /// with the given <paramref name="content"/> and optional 
    /// <paramref name="fontSize"/>.</summary>
    public Text(string content, double x, double y, double fontSize = 12)
    {
        this.Content = content;
        this.Position = new Point(x, y);
        this.FontSize = fontSize;
    }

    /// <summary>The bounding box of the text, accounting for justification offsets.</summary>
    public override Rect Bounds
    {
        get
        {
            var ft = this.CreateFormattedText();
            return new Rect(
                this.Position.X - this.HJust * ft.Width,
                this.Position.Y - this.VJust * ft.Height,
                ft.Width,
                ft.Height
            );
        }
    }

    /// <summary>Scales the text by multiplying <see cref="FontSize"/> by <paramref name="factor"/>.</summary>
    public override void Scale(double factor)
    {
        this.FontSize *= factor;
    }

    /// <summary>Translates the anchor point by (<paramref name="dx"/>, <paramref name="dy"/>).</summary>
    public override void Move(double dx, double dy)
    {
        this.Position = new Point(this.Position.X + dx, this.Position.Y + dy);
    }

    /// <summary>Renders the text into <paramref name="ctx"/>, applying a counter-transform to
    /// compensate for the drawing's flipped Y-axis and an optional rotation.</summary>
    protected override void Draw(DrawingContext ctx)
    {
        var bounds = this.Bounds;
        var transform =
                Matrix.CreateTranslation(bounds.X, -bounds.Y - bounds.Height).Append(
                Matrix.CreateScale(1, -1)).Append(
                Matrix.CreateRotation(this.Angle * Math.PI / 180, this.Position))
            ;

        using (ctx.PushTransform(transform))
        {
            ctx.DrawText(this.CreateFormattedText(), new Point(0, 0));
        }
    }

    /// <summary>Returns a copy of this text shifted by (<paramref name="dx"/>, <paramref name="dy"/>).</summary>
    public new Text Copy(double dx, double dy)
    {
        return (Text)base.Copy(dx, dy);
    }

    private FormattedText CreateFormattedText()
    {
        return new FormattedText(
            this.Content,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            this.Typeface,
            this.FontSize,
            new SolidColorBrush(this.Color)
        );
    }
}
