using BoDraw;

public class Demo03
{

    const int NC = 8;
    const double SX = 20;
    const double SY = 5;
    const double DX = 2;
    const double DY = 5;

    public static void Draw(IBoDraw bd)
    {
        for (int i = 0; i < Colors.Count; i++)
        {
            double x = i % NC * (SX + DX);
            double y = -i / NC * (SY + DY);
            var r = new Rectangle(x, y, x + SX, y + SY);
            var t = new Text(Colors.Name(i), x, y - 3, 2);

            r.FillColor = Colors.Color(i);
            bd.Add(r, t);
        }
    }
}