using BoDraw;

public class FitIntoDemo
{
    public static void Draw(IBoDraw bd)
    {
        Rectangle r1 = new Rectangle(6, -1.8, 8, 6);
        Rectangle r2 = new Rectangle(9, -1.8, 11, 6);

        Polygon star = new Polygon(
            3, 4.7, 3.5, 2.9, 5, 2.9, 3.8, 1.8,
            4.2, 0, 3, 1.0, 1.8, 0, 2.2, 1.8,
            1, 2.9, 2.5, 2.9
        );
        star.FillColor = Colors.BlanchedAlmond;

        bd.Add(r1);
        bd.Add(r2);
        bd.Add(star);
        bd.Add(star.Copy().FitInto(r1));
        bd.Add(star.Copy().FitInto(r2, keepAspect: true));
    }
}
