using BoDraw;

public class DimensioningDemo
{
    public static void Draw(IBoDraw bd)
    {
        // First 
        Group g1 = new Group();
        Dimensioning dim = new Dimensioning(0.5);
        g1.Add(dim, new Polygon(0, 0, 3, 0, 5, 1, 6, 1, 6, 6, 0, 6));
        dim.ScalingFactor = 100;
        dim.Format = "0.##cm";
        dim.Start(0, -1.5);
        dim.HStep(6);
        dim.StartNext();
        dim.HStep(3, 5, 6);
        dim.Start(7.5, 0);
        dim.VStep(6);
        dim.StartNext();
        dim.VStep(1, 6);

        // Make a copy and move
        Group g2 = g1.Copy(10, 0);

        bd.Add(g1, g2);
    }
}