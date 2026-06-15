using BoDraw;

public class GridDemo
{
    public static void Draw(IBoDraw bd)
    {
        Rectangle r1 = new Rectangle(0, 0, 1.5, 2.0 / 3.0);
        r1.FillColor = Colors.HotPink;

        Rectangle r2 = new Rectangle(0, 1.0 / 3.0, 1.5, 1);
        r2.FillColor = Colors.Orange;
        r2.FillOpacity = 0.7;

        Circle c = new Circle(0.5, 0.5, 0.5);
        c.FillColor = Colors.FromRgb(20, 255, 140);
        c.FillOpacity = 0.3;

        Group group = new Group(r1, r2, c);

        Grid grid = new Grid(group);
        grid.Nx = 10;
        grid.Dx = 1.6;
        grid.Ny = 15;
        grid.Dy = 1.1;

        bd.Add(grid);
    }
}