using BoDraw;

internal class DimensioningDemo
{
    internal static void Draw(BoDrawApp bd)
    {
        Polygon p = new Polygon(0,0, 3,0, 5,1, 6,1, 6,6, 0,6);

        Dimensioning dim = new Dimensioning(0.5);
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

        bd.Add(p, dim);
    }
}