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

    /// <summary>A single pixel within an image, identified by row and column.</summary>
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

        /// <summary>The zero-based row index, from bottom to top.</summary>
        public int Row
        {
            get { return this.row; }
        }

        /// <summary>The zero-based column index, from left to right.</summary>
        public int Col
        {
            get { return this.col; }
        }

        /// <summary>The X coordinate of the pixel center in drawing units.</summary>
        public double X
        {
            get { return this.image.bounds.X + this.NCol * this.image.bounds.Width; }
        }

        /// <summary>The Y coordinate of the pixel center in drawing units.</summary>
        public double Y
        {
            get { return this.image.bounds.Y + this.NRow * this.image.bounds.Height; }
        }

        internal double NRow
        {
            get { return (this.row + 0.5) / this.image.bitmap.PixelSize.Height; }
        }

        internal double NCol
        {
            get { return (this.col + 0.5) / this.image.bitmap.PixelSize.Width; }
        }

        /// <summary>The color of this pixel.</summary>
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

    /// <summary>Loads an image from a file, placed within the rectangle from (x, y) with the given width. Height defaults to preserving the aspect ratio.</summary>
    public Image(string imagePath, double x, double y, double width, double height = 0)
        :
        this(new Bitmap(ResolveImage(imagePath)), x, y, width, height)
    { }

    /// <summary>Creates a blank writable image covering the rectangle defined by two corners, with the given pixel width.</summary>
    public Image(double x1, double y1, double x2, double y2, int pixelWidth)
        :
        this(
            new WriteableBitmap(
                new PixelSize(pixelWidth, (int)(Math.Abs(y2 - y1) / Math.Abs(x2 - x1) * pixelWidth)), new(96, 96), PixelFormat.Rgba8888
            ),
            Math.Min(x1, x2), Math.Min(y1, y2), Math.Abs(x2 - x1), Math.Abs(y2 - y1)
        )
    { }

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

    /// <summary>The bounding rectangle of the image in drawing units.</summary>
    public override Rect Bounds
    {
        get { return this.bounds; }
    }

    /// <summary>The size of the image in pixels.</summary>
    public PixelSize PixelSize
    {
        get { return this.bitmap.PixelSize; }
    }

    public override void ApplyTransform(Matrix t)
    {
        this.bounds = this.bounds.ApplyTransform(t);
    }


    /// <summary>Returns a copy of this image shifted by (dx, dy).</summary>
    public new Image Copy(double dx, double dy)
    {
        return (Image)base.Copy(dx, dy);
    }

    /// <summary>Returns the pixel at the given row and column.</summary>
    public Pixel PixelAt(int row, int col)
    {
        return new Pixel(this, row, col);
    }

    /// <summary>Enumerates all pixels row by row, from bottom to top.</summary>
    /// <remarks>The same Pixel instance is reused on each iteration — do not keep references across iterations.</remarks>
    public IEnumerable<Pixel> Pixels
    {
        get
        {
            var pixel = new Pixel(this, 0, 0);
            for (int row = 0; row < this.bitmap.PixelSize.Height; row++)
            {
                for (int col = 0; col < this.bitmap.PixelSize.Width; col++)
                {
                    pixel.row = row;
                    pixel.col = col;
                    yield return pixel;
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
