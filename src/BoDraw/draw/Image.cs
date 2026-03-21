using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace bodraw;

/// <summary>
/// A raster image loaded from a file and placed at a given position with a specified width.
/// The height is derived from the image's aspect ratio when not explicitly set.
/// </summary>
public class Image : SimpleShape
{
    private Rect bounds;
    private readonly Bitmap bitmap;

    public double X
    {
        get { return this.bounds.X; }
        set { this.bounds = new Rect(value, this.bounds.Y, this.bounds.Width, this.bounds.Height); }
    }

    public double Y
    {
        get { return this.bounds.Y; }
        set { this.bounds = new Rect(this.bounds.X, value, this.bounds.Width, this.bounds.Height); }
    }

    public double Width
    {
        get { return this.bounds.Width; }
        set { this.bounds = new Rect(this.bounds.X, this.bounds.Y, value, 1 / this.Aspect * value); }
    }

    public double Height
    {
        get { return this.bounds.Height; }
        set { this.bounds = new Rect(this.bounds.X, this.bounds.Y, this.Aspect * value, value); }
    }

    public double Aspect
    {
        get { return this.bounds.Width / this.bounds.Height; }
    }

    public override Rect Bounds => this.bounds;

    public Image(string filePath, double x, double y, double width, double height = 0)
    {
        this.bitmap = new Bitmap(filePath);
        if (height == 0)
        {
            var size = this.bitmap.Size;
            height = width * size.Height / size.Width;
        }
        this.bounds = new Rect(x, y, width, height);
    }

    protected override void Draw(DrawingContext ctx)
    {
        // Counter-transform the global Y-flip so the image renders right-side up
        var transform = Matrix.CreateTranslation(this.Bounds.X, -this.Bounds.Y - this.Bounds.Height)
            .Append(Matrix.CreateScale(1, -1));

        using (ctx.PushTransform(transform))
        {
            ctx.DrawImage(this.bitmap, new Rect(0, 0, this.Bounds.Width, this.Bounds.Height));
        }
    }
}
