using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace BoDraw;

/// <summary>
/// A raster image placed at a given position with a specified size.
/// Can be loaded from a file or created programmatically via SetPixel.
/// </summary>
public class Image : SimpleShape
{

    public class Pixel
    {
        private Image image;
        internal int row;
        internal int col;

        internal Pixel(Image image, int row, int col)
        {
            this.image = image;
            this.row = row;
            this.col = col;
        }

        public int Row => this.row;
        public int Col => this.col;

        public double X => this.image.bounds.X + this.NCol * this.image.bounds.Width;
        public double Y => this.image.bounds.Y + this.NRow * this.image.bounds.Height;

        internal double NRow => (this.row + 0.5) / this.image.bitmap.PixelSize.Height;
        internal double NCol => (this.col + 0.5) / this.image.bitmap.PixelSize.Width;

        public Color Color
        {
            get
            {
                return this.image.bitmap.ColorAt(this.col, this.image.PixelSize.Height - this.row - 1);
            }

            set
            {
                WriteableBitmap bitmap = (WriteableBitmap)this.image.bitmap;
                using var fb = bitmap.Lock();
                byte[] pixel = [value.R, value.G, value.B, value.A];
                int bitmapRow = this.image.bitmap.PixelSize.Height - this.row - 1;
                Marshal.Copy(pixel, 0, fb.Address + bitmapRow * fb.RowBytes + this.col * 4, 4);
            }
        }
    }

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

    private Image(Bitmap bitmap, double x, double y, double width, double height)
    {
        this.bitmap = bitmap;
        this.bounds = new Rect(x, y, width, height == 0 ? width * bitmap.Size.Height / bitmap.Size.Width : height);
    }

    public Image(string imagePath, double x, double y, double width, double height = 0)
        :
        this(new Bitmap(ResolveImage(imagePath)), x, y, width, height)
    {
    }

    public Image(int pixelWidth, int pixelHeight, double x, double y, double width, double height = 0)
        :
        this(
            new WriteableBitmap(
                new PixelSize(pixelWidth, pixelHeight), new(96, 96), PixelFormat.Rgba8888
            ),
            x, y, width, height
        )
    {
    }

    /// <summary>The X coordinate of the lower-left corner.</summary>
    public double X
    {
        get { return this.bounds.X; }
        set { this.bounds = this.bounds.WithX(value); }
    }

    /// <summary>The Y coordinate of the lower-left corner.</summary>
    public double Y
    {
        get { return this.bounds.Y; }
        set { this.bounds = this.bounds.WithY(value); }
    }

    /// <summary>The width in drawing units.</summary>
    public double Width
    {
        get { return this.bounds.Width; }
        set { this.bounds = this.bounds.WithWidth(value); }
    }

    /// <summary>The height in drawing units.</summary>
    public double Height
    {
        get { return this.bounds.Height; }
        set { this.bounds = this.bounds.WithHeight(value); }
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

    public PixelSize PixelSize
    {
        get { return this.bitmap.PixelSize; }
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

    public Pixel PixelAt(int row, int col)
    {
        return new Pixel(this, row, col);
    }

    public IEnumerable<Pixel> Pixels
    {
        get
        {
            for (int row = 0; row < this.bitmap.PixelSize.Height; row++)
            {
                for (int col = 0; col < this.bitmap.PixelSize.Width; col++)
                {
                    yield return new Pixel(this, row, col);
                }
            }
        }
    }

    protected override void Draw(DrawingContext ctx)
    {
        // Counter-transform the global Y-flip so the image renders right-side up
        var transform = Matrix.
            CreateTranslation(this.Bounds.X, -this.Bounds.Y - this.Bounds.Height).
            Append(Matrix.CreateScale(1, -1));

        using (ctx.PushTransform(transform))
        {
            ctx.DrawImage(
                this.bitmap,
                new Rect(0, 0, this.Bounds.Width, this.Bounds.Height)
            );
        }
    }
}
