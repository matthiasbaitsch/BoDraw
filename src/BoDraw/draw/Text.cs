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

    /// <summary>The font size.</summary>
    public double FontSize { get; set; }

    /// <summary>Horizontal justification: 0 = left, 0.5 = center, 1 = right.</summary>
    public double HJust { get; set; } = 0;

    /// <summary>Vertical justification: 0 = bottom, 0.5 = middle, 1 = top.</summary>
    public double VJust { get; set; } = 0;

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

    public override void Scale(double factor)
    {
        this.FontSize *= factor;
    }

    public override void Move(double dx, double dy)
    {
        this.Position = new Point(this.Position.X + dx, this.Position.Y + dy);
    }

    protected override void Draw(DrawingContext ctx)
    {
        var bounds = this.Bounds;
        var transform = Matrix.CreateTranslation(bounds.X, -bounds.Y - bounds.Height).Append(Matrix.CreateScale(1, -1));

        using (ctx.PushTransform(transform))
        {
            ctx.DrawText(this.CreateFormattedText(), new Point(0, 0));
        }
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
