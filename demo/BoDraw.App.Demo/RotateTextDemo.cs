using Avalonia;
using BoDraw;

public class RotateTextDemo
{
    public static void Draw(IBoDraw bd)
    {
        #region snippet

        Text t = new Text("Hello World!", 100, 0);

        int n = 20;
        Point c = new Point(0, 0);

        for (int i = 0; i < n; i++)
        {
            bd.Add(t.Copy().Rotate(i * 360.0 / n, c));
        }

        #endregion
    }
}