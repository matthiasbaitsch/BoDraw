using BoDraw;

public class ArrowDemo
{
    public static void Draw(IBoDraw bd)
    {
        // Thin black arrows in various directions
        bd.Add(new Arrow(0, 0, 20, 0));
        bd.Add(new Arrow(0, 0, 0, 15));
        bd.Add(new Arrow(0, 0, 20, 15));
        bd.Add(new Arrow(0, 15, 20, 0));

        // Colored arrows with varying thickness
        Arrow a1 = new Arrow(24, 0, 44, 15);
        a1.Color = Colors.CornflowerBlue;
        a1.Thickness = 2;

        Arrow a2 = new Arrow(24, 15, 44, 0);
        a2.Color = Colors.OrangeRed;
        a2.Thickness = 3;

        Arrow a3 = new Arrow(24, 7.5, 44, 7.5);
        a3.Color = Colors.SeaGreen;
        a3.Thickness = 0.5;

        bd.Add(a1, a2, a3);
    }
}
