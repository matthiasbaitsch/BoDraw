using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Renders a shape repeatedly in a rectangular grid of <see cref="Nx"/> columns
/// and <see cref="Ny"/> rows, with cell spacing <see cref="Dx"/> and <see cref="Dy"/>.
/// Use <see cref="Group"/> to place multiple shapes per cell.
/// </summary>
/// <example>
/// <code>
/// using BoDraw;
///
/// Grid grid = new Grid(new Circle(0.5, 0.5, 0.4));
/// grid.Nx = 4;
/// grid.Dx = 1.5;
/// grid.Ny = 3;
/// grid.Dy = 1.5;
///
/// BoDrawApp bd = new BoDrawApp();
/// bd.Add(grid);
/// bd.Show();
/// </code>
/// </example>
public class Grid : Shape
{
    /// <summary>Number of columns.</summary>
    public int Nx { set; get; } = 1;

    /// <summary>Number of rows.</summary>
    public int Ny { set; get; } = 1;

    /// <summary>Horizontal distance between cell origins.</summary>
    public double Dx { set; get; } = 0;

    /// <summary>Vertical distance between cell origins.</summary>
    public double Dy { set; get; } = 0;

    private readonly Shape shape;
    private Matrix transform = Matrix.Identity;

    /// <summary>Creates a grid that repeats <paramref name="shape"/>. Configure
    /// <see cref="Nx"/>, <see cref="Ny"/>, <see cref="Dx"/>, <see cref="Dy"/> after construction.</summary>
    public Grid(Shape shape)
    {
        this.shape = shape;
    }

    public override Rect Bounds
    {
        get
        {
            var b = this.shape.Bounds;
            var gridBounds = new Rect(
                b.X,
                b.Y,
                b.Width + (this.Nx - 1) * this.Dx,
                b.Height + (this.Ny - 1) * this.Dy
            );
            return gridBounds.TransformToAABB(this.transform);
        }
    }

    public override void Move(double dx, double dy)
    {
        this.transform = this.transform.Append(Matrix.CreateTranslation(dx, dy));
    }

    public override void Scale(double factor)
    {
        var c = this.Bounds.Center;
        this.transform = this.transform
            .Append(Matrix.CreateTranslation(-c.X, -c.Y))
            .Append(Matrix.CreateScale(factor, factor))
            .Append(Matrix.CreateTranslation(c.X, c.Y));
    }

    internal override void Draw(double scale, DrawingContext ctx)
    {
        using (ctx.PushTransform(this.transform))
        {
            for (int row = 0; row < this.Ny; row++)
            {
                for (int col = 0; col < this.Nx; col++)
                {
                    var t = Matrix.CreateTranslation(col * this.Dx, row * this.Dy);
                    using (ctx.PushTransform(t))
                    {
                        this.shape.Draw(scale, ctx);
                    }
                }
            }
        }
    }
}
