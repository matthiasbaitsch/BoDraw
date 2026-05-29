using BoDraw;

internal class ImageWriteDemo
{
    internal static void Draw(BoDrawApp bd)
    {
        Image image = new Image(-1.1, -1.1, 1.1, 1.1, 1500);

        foreach (var p in image.Pixels)
        {
            if (p.X * p.X + p.Y * p.Y <= 1)
            {
                if (p.X >= 0 && p.Y >= 0)
                {
                    p.Color = Colors.Orange;
                }
                else
                {
                    p.Color = Colors.LightBlue;
                }
            }
            else
            {
                if (p.X >= 0 && p.Y >= 0)
                {
                    p.Color = Colors.Bisque;
                }
                else
                {
                    p.Color = Colors.Coral;
                }
            }
        }

        bd.Add(image);
    }
}