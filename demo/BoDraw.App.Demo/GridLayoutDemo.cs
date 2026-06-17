using BoDraw;

public class GridLayoutDemo
{
    public static void Draw(IBoDraw bd)
    {
        GridLayout g = new GridLayout();
        g.HGap = 0.5;
        g.VGap = 0.3;

        // Row 0: large rectangle, large ellipse, medium ellipse
        g.Add(0, 0, new Rectangle(-4, -2, 4, 2).WithFillColor(Colors.SteelBlue));
        g.Add(0, 1, new Ellipse(0, 0, 3, 2).WithFillColor(Colors.MediumPurple));
        g.Add(0, 2, new Ellipse(0, 0, 2.5, 1.5).WithFillColor(Colors.MediumSeaGreen));

        // Row 1: empty, medium circle, empty
        g.Add(1, 1, new Circle(0, 0, 2).WithFillColor(Colors.Orange));

        // Row 2: small ellipse, medium rectangle, large circle
        g.Add(2, 0, new Ellipse(0, 0, 1.5, 1).WithFillColor(Colors.Coral));
        g.Add(2, 1, new Rectangle(-2, -1.5, 2, 1.5).WithFillColor(Colors.CornflowerBlue));
        g.Add(2, 2, new Circle(0, 0, 3).WithFillColor(Colors.LightSalmon));

        bd.Add(g);
    }
}
