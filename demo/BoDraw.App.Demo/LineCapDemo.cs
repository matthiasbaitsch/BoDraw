using BoDraw;

public class LineCapDemo
{
    public static void Draw(IBoDraw bd)
    {
        #region snippet
        Avalonia.Media.PenLineCap[] caps =
        [
            Avalonia.Media.PenLineCap.Flat,
            Avalonia.Media.PenLineCap.Round,
            Avalonia.Media.PenLineCap.Square,
        ];

        for (int i = 0; i < caps.Length; i++)
        {
            PathShape line = new PathShape();
            line.MoveTo(0, i * 40);
            line.LineTo(150, i * 40);
            line.FillColor = null;
            line.LineColor = Colors.DarkSlateBlue;
            line.LineThickness = 20;
            line.LineCap = caps[i];
            bd.Add(line);
        }
        #endregion
    }
}
