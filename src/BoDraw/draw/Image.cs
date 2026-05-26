using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace BoDraw;

/// <summary>
/// A raster image loaded from a file and placed at a given position with a specified width.
/// The height is derived from the image's aspect ratio when not explicitly set.
/// </summary>
public class Image : SimpleShape
{
    private static string ResolveImage(string imagePath)
    {
        string[] prefixes = ["../../../..", "../../..", "../..", ".."];
        foreach (string p in prefixes)
        {
            string ip = Path.Combine(p, imagePath);
            if (File.Exists(ip))
            {
                return ip;
            }
        }
        return imagePath;
    }

    private Rect bounds;
    private readonly Bitmap bitmap;

    public Image(string imagePath, double x, double y, double width, double height = 0)
    {
        this.bitmap = new Bitmap(ResolveImage(imagePath));
        if (height == 0)
        {
            var size = this.bitmap.Size;
            height = width * size.Height / size.Width;
        }
        this.bounds = new Rect(x, y, width, height);
    }

    /// <summary>The X coordinate of the lower-left corner.</summary>
    public double X
    {
        get { return this.bounds.X; }
        set { this.bounds = new Rect(value, this.bounds.Y, this.bounds.Width, this.bounds.Height); }
    }

    /// <summary>The Y coordinate of the lower-left corner.</summary>
    public double Y
    {
        get { return this.bounds.Y; }
        set { this.bounds = new Rect(this.bounds.X, value, this.bounds.Width, this.bounds.Height); }
    }

    /// <summary>The width in drawing units. Setting this also adjusts Height to preserve the aspect ratio.</summary>
    public double Width
    {
        get { return this.bounds.Width; }
        set { this.bounds = new Rect(this.bounds.X, this.bounds.Y, value, 1 / this.Aspect * value); }
    }

    /// <summary>The height in drawing units. Setting this also adjusts Width to preserve the aspect ratio.</summary>
    public double Height
    {
        get { return this.bounds.Height; }
        set { this.bounds = new Rect(this.bounds.X, this.bounds.Y, this.Aspect * value, value); }
    }

    /// <summary>The width-to-height ratio of the image.</summary>
    public double Aspect
    {
        get { return this.bounds.Width / this.bounds.Height; }
    }

    public override Rect Bounds
    {
        get { return this.bounds; }
    }

    public override void Scale(double factor)
    {
        this.bounds = this.bounds.Scale(factor);
    }

    public override void Move(double dx, double dy)
    {
        this.bounds = this.bounds.Move(dx, dy);
    }

    public new Image Copy(double dx, double dy)
    {
        return (Image)base.Copy(dx, dy);
    }

    protected override void Draw(DrawingContext ctx)
    {
        // Counter-transform the global Y-flip so the image renders right-side up
        var transform = Matrix.
            CreateTranslation(this.Bounds.X, -this.Bounds.Y - this.Bounds.Height).
            Append(Matrix.CreateScale(1, -1));

        using (ctx.PushTransform(transform))
        {
            ctx.DrawImage(this.bitmap, new Rect(0, 0, this.Bounds.Width, this.Bounds.Height));
        }
    }
}
