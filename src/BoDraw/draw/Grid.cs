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
/// BoDrawApp app = new BoDrawApp();
/// 
/// Rectangle r1 = new Rectangle(0, 0, 1.5, 2.0 / 3.0);
/// r1.FillColor = Colors.HotPink;
///
/// Rectangle r2 = new Rectangle(0, 1.0 / 3.0, 1.5, 1);
/// r2.FillColor = Colors.Orange;
/// r2.FillOpacity = 0.7;
///
/// Circle c = new Circle(0.5, 0.5, 0.5);
/// c.FillColor = Colors.FromRgb(20, 255, 140);
/// c.FillOpacity = 0.3;
///
/// Group group = new Group(r1, r2, c);
///
/// Grid grid = new Grid(group);
/// grid.Nx = 10;
/// grid.Dx = 1.6;
/// grid.Ny = 15;
/// grid.Dy = 1.1;
///
/// app.Add(grid);
/// app.Show();
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

    public override void ApplyTransform(Matrix t)
    {
        this.transform = this.transform.Append(t);
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
