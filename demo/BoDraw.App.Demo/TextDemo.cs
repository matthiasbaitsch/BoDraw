using BoDraw;

internal class TextDemo
{
    internal static void Draw(BoDrawApp bd)
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

        Text t4 = new Text("Langer Text \nmit Zeilenumbrüchen \nist linksbündig ausgerichtet <sup>33</sup>", 150, 0, s);

        Text t5 = new Text("Gedrehter Text", 150, 50, s);
        t5.Angle = 30;

        bd.Add(dotit(t1), dotit(t2), dotit(t3), dotit(t4), dotit(t5));
    }
}