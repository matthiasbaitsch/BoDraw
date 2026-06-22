using BoDraw;

public class TextDemo
{
    public static void Draw(IBoDraw bd)
    {
        static Shape dotit(Text t)
        {
            Circle c = new Circle(t.X, t.Y, 0.06 * t.FontSize);
            c.FillColor = Colors.HotPink;
            return new Group(c, t);
        }

        double s = 10;

        Text t1 = new Text("Linksbündig: HJust = 0", 0, 0, s);
        t1.HJust = 0;
        Text t2 = new Text("Zentriert: HJust = 0.5", 0, 1.3 * s, s);
        t2.HJust = 0.5;
        Text t3 = new Text("Rechtsbündig: HJust = 1", 0, 2.6 * s, s);
        t3.HJust = 1;

        Text t4 = new Text("Langer Text", 150, 0, s);
        t4.AppendLine("mit Zeilenumbrüchen");
        t4.AppendLine("via AppendLine");

        Text t5 = new Text("Gedrehter Text", 150, 50, s);
        t5.Angle = 30;

        Text t6 = new Text(150, 80, s);
        t6.Content = "Courier New Schrift";
        t6.FontFamilyName = "Courier New";

        Text t7 = new Text("Text mit Kopie", 0, 50, s);
        Shape t8 = t7.Copy(0, 0);
        t8.Scale(1.5);
        t7.Color = Colors.SteelBlue;

        bd.Add(
            dotit(t1), dotit(t2), dotit(t3), dotit(t4), dotit(t5),
            dotit(t6), dotit(t7),
            t8
        );
    }
}