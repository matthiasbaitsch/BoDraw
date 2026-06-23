using Avalonia;
using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Arranges shapes in a grid of rows and columns.
/// Each row is sized to the tallest shape in that row; each column to the widest shape in that
/// column. Shapes smaller than their cell are centered automatically. Cells may be left empty.
/// </summary>
/// 
/// <example>
/// <code>
/// GridLayout g = new GridLayout();
/// g.HGap = 0.5;
/// g.VGap = 0.3;
/// g.Add(0, 0, new Rectangle(-4, -2, 4, 2).WithFillColor(Colors.SteelBlue));
/// g.Add(0, 1, new Ellipse(0, 0, 3, 2).WithFillColor(Colors.MediumPurple));
/// g.Add(0, 2, new Ellipse(0, 0, 2.5, 1.5).WithFillColor(Colors.MediumSeaGreen));
/// g.Add(1, 1, new Circle(0, 0, 2).WithFillColor(Colors.Orange));
/// g.Add(2, 0, new Ellipse(0, 0, 1.5, 1).WithFillColor(Colors.Coral));
/// g.Add(2, 1, new Rectangle(-2, -1.5, 2, 1.5).WithFillColor(Colors.CornflowerBlue));
/// g.Add(2, 2, new Circle(0, 0, 3).WithFillColor(Colors.LightSalmon));
/// bd.Add(g);
/// </code>
/// </example>
public class GridLayout : Shape
{
    private record Entry(Shape Shape, int Row, int Col);

    private record Layout(double[] ColWidths, double[] RowHeights, double[] ColX, double[] RowY);

    private Matrix transform;
    private readonly List<Entry> entries;

    /// <summary>Horizontal gap between columns in drawing units.</summary>
    public double HGap { get; set; } = 0;

    /// <summary>Vertical gap between rows in drawing units.</summary>
    public double VGap { get; set; } = 0;

    public GridLayout() : this([], Matrix.Identity, 0, 0) { }

    private GridLayout(List<Entry> entries, Matrix transform, double hGap, double vGap)
    {
        this.entries = entries;
        this.transform = transform;
        this.HGap = hGap;
        this.VGap = vGap;
    }

    /// <summary>Places <paramref name="s"/> in the cell at the given row and column.</summary>
    public void Add(int row, int col, Shape s)
    {
        this.entries.Add(new Entry(s, row, col));
    }

    private Layout ComputeLayout()
    {
        int numRows = this.entries.Max(e => e.Row) + 1;
        int numCols = this.entries.Max(e => e.Col) + 1;

        double[] colWidths = new double[numCols];
        double[] rowHeights = new double[numRows];

        foreach (var entry in this.entries)
        {
            var b = entry.Shape.Bounds;
            colWidths[entry.Col] = Math.Max(colWidths[entry.Col], b.Width);
            rowHeights[entry.Row] = Math.Max(rowHeights[entry.Row], b.Height);
        }

        double[] colX = new double[numCols];
        for (int c = 1; c < numCols; c++)
        {
            colX[c] = colX[c - 1] + colWidths[c - 1] + this.HGap;
        }

        double[] rowY = new double[numRows];
        for (int r = 1; r < numRows; r++)
        {
            rowY[r] = rowY[r - 1] + rowHeights[r - 1] + this.VGap;
        }

        return new Layout(colWidths, rowHeights, colX, rowY);
    }

    public override Rect Bounds
    {
        get
        {
            if (this.entries.Count == 0)
            {
                return new Rect(0, 0, 0, 0);
            }

            var layout = this.ComputeLayout();
            double totalWidth = layout.ColWidths.Sum() + this.HGap * (layout.ColWidths.Length - 1);
            double totalHeight = layout.RowHeights.Sum() + this.VGap * (layout.RowHeights.Length - 1);
            return new Rect(0, 0, totalWidth, totalHeight).TransformToAABB(this.transform);
        }
    }

    public override Shape ApplyTransform(Matrix t)
    {
        this.transform = this.transform.Append(t);
        return this;
    }

    protected internal override Shape DeepClone()
    {
        return new GridLayout([.. this.entries], this.transform, this.HGap, this.VGap);
    }

    internal override void Draw(double scale, DrawingContext ctx)
    {
        if (this.entries.Count == 0)
        {
            return;
        }

        var layout = this.ComputeLayout();

        using (ctx.PushTransform(this.transform))
        {
            foreach (var entry in this.entries)
            {
                var b = entry.Shape.Bounds;
                double cellCenterX = layout.ColX[entry.Col] + layout.ColWidths[entry.Col] / 2;
                double cellCenterY = layout.RowY[entry.Row] + layout.RowHeights[entry.Row] / 2;
                double dx = cellCenterX - b.Center.X;
                double dy = cellCenterY - b.Center.Y;

                using (ctx.PushTransform(Matrix.CreateTranslation(dx, dy)))
                {
                    entry.Shape.Draw(scale, ctx);
                }
            }
        }
    }
}
