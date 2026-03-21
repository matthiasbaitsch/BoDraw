using System.Globalization;
using Avalonia;
using Avalonia.Media;

namespace bodraw;

public class Text : SimpleShape
{
    public string Content { get; set; }
    public Point Position { get; set; }
    public double FontSize { get; set; }
    public double HJust { get; set; } = 0; // 0: left, 0.5: center, 1: right
    public double VJust { get; set; } = 0; // 0: bottom, 0.5: middle, 1: top
    public Typeface Typeface { get; set; } = new Typeface("Arial");
    public Color Color { get; set; } = Colors.Black;

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
                ft.Height);
        }
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
