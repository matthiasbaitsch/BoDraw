using BoDraw;

public class ColorTableDemo
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

            bd.Add(
                new Rectangle(x, y, x + SX, y + SY).WithFillColor(Colors.Color(i)),
                new Text(Colors.Name(i), x, y - 3, 2)
            );
        }
    }
}
