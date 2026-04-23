using BoDraw;

internal class Demo1
{
    internal static void Draw(BoDrawApp bd)
    {
        Image image = new Image("assets/hs-bo_logo_en.png", 0, 0, 400);

        Rectangle rectangle = new Rectangle(-20, -20, image.Width + 20, image.Height + 20);
        rectangle.LineThickness = 3;
        rectangle.FillColor = Colors.LightSkyBlue;
        rectangle.LineColor = null;

        Text text = new Text("BoDraw ist ein einfaches Zeichenpaket...", -20, -40);
        text.Color = Colors.DarkOliveGreen;
        text.FontSize = 8;

        Polygon star = new Polygon();
        star.AddPoint(0, 55);
        star.AddPoint(7, 17);
        star.AddPoint(27, 27);
        star.AddPoint(14, 6);
        star.AddPoint(50, 0);
        star.AddPoint(18, -9);
        star.AddPoint(27, -32);
        star.AddPoint(4, -11);
        star.AddPoint(-5, -52);
        star.AddPoint(-7, -15);
        star.AddPoint(-25, -25);
        star.AddPoint(-14, -6);
        star.AddPoint(-48, 0);
        star.AddPoint(-17, 7);
        star.AddPoint(-28, 28);
        star.AddPoint(-6, 15);
        star.Scale(0.25);
        star.FillColor = Colors.Yellow;
        star.LineColor = Colors.Red;
        star.LineThickness = 2;
        star.Move(-20, -20);

        bd.Add(
            text,
            rectangle,
             star,
             star.Copy(image.Width + 40, 0),
             star.Copy(0, image.Height + 40),
             star.Copy(image.Width + 40, image.Height + 40),
             image);
    }
}