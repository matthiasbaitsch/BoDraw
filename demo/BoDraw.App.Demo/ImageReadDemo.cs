using BoDraw;

internal class ImageReadDemo
{
    internal static void Draw(BoDrawApp bd)
    {
        double W = 500;
        Image image = new Image("assets/hs-bo_logo_en.png", 0, 0, W);

        int NP = 10;
        double D = W / (image.PixelSize.Width / NP);
        int NN = NP * NP;

        Group gg = new Group();
        gg.Move(0, 1.1 * image.Height);

        for (int col = 0; col < image.PixelSize.Width - NP; col += NP)
        {
            for (int row = 0; row < image.PixelSize.Height - NP; row += NP)
            {
                double x = 0, y = 0;
                int r = 0, g = 0, b = 0, a = 0;
                for (int offrow = 0; offrow < NP; offrow++)
                {
                    for (int offcol = 0; offcol < NP; offcol++)
                    {
                        var p = image.PixelAt(row + offrow, col + offcol);
                        var c = p.Color;
                        x += p.X;
                        y += p.Y;
                        r += c.R;
                        g += c.G;
                        b += c.B;
                        a += c.A;
                    }
                }
                Circle cc = new Circle(x / NN, y / NN, D / 2.2);
                cc.FillColor = Colors.FromArgb(a / NN, r / NN, g / NN, b / NN);
                cc.LineColor = null;
                gg.Add(cc);
            }
        }

        bd.Add(image, gg);
    }
}
