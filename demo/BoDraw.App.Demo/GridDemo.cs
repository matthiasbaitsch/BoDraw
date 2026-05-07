using BoDraw;

internal class GridDemo
{
    internal static void Draw(BoDrawApp bd)
    {
        Rectangle r1 = new Rectangle(0, 0, 1.5, 2.0 / 3.0);
        r1.FillColor = Colors.HotPink;

        Rectangle r2 = new Rectangle(0, 1.0 / 3.0, 1.5, 1);
        r2.FillColor = Colors.Orange;
        r2.FillOpacity = 0.7;

        Circle c = new Circle(0.5, 0.5, 0.5);
        c.FillColor = Colors.FromRgb(20, 255, 140);
        c.FillOpacity = 0.3;

        Grid gg = new Grid(new Group(r1, r2, c));
        gg.Nx = 10;
        gg.Dx = 1.6;
        gg.Ny = 15;
        gg.Dy = 1.1;

        bd.Add(gg);
    }
}