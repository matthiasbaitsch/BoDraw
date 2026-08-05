using BoDraw;

public class PathShapeDemo
{
    public static void Draw(IBoDraw bd)
    {
        #region snippet
        PathShape rounded = new PathShape("M 0,0 L 120,0 A 60,60 0 0 1 120,120 L 0,120 Z");
        rounded.FillColor = Colors.BlanchedAlmond;
        rounded.LineColor = Colors.Magenta;
        rounded.LineThickness = 3;

        PathShape wave = new PathShape();
        wave.MoveTo(0, 0);
        wave.QuadTo(30, 60, 60, 0);
        wave.QuadTo(90, -60, 120, 120);
        wave.LineColor = Colors.DarkMagenta;
        wave.LineThickness = 5;
        wave.FillColor = null;

        bd.Add(rounded, wave);
        #endregion
    }
}
