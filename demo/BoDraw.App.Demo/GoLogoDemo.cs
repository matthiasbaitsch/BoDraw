using BoDraw;

public class GoLogoDemo
{
    public static void Draw(IBoDraw bd)
    {
        #region snippet
        int n = 6;
        double h = 20;
        double r = 20;
        double d = 2.5;
        double w = (n - 1) * d / 2;
        double a = 50;

        PathShape letterG = new PathShape();
        PathShape letterO = new PathShape();

        letterG.FillColor = null;
        letterO.FillColor = Colors.SteelBlue;
        letterG.LineThickness = letterO.LineThickness = 5;

        for (int i = 0; i < n; i++)
        {
            letterG.MoveTo(r, h / 2);
            letterG.ArcTo(-r, h / 2, r);
            letterG.LineTo(-r, -h / 2);
            letterG.ArcTo(r, -h / 2, r);
            letterG.LineTo(r, w);
            letterG.LineTo(0, w);

            letterO.MoveTo(r + a, h / 2);
            letterO.ArcTo(-r + a, h / 2, r);
            letterO.LineTo(-r + a, -h / 2);
            letterO.ArcTo(r + a, -h / 2, r);
            letterO.Close();

            r -= d;
            w -= d;
        }

        bd.Add(letterG, letterO);
        #endregion
    }
}
