using BoDraw;
using System.Reflection;
using Avalonia.Media;

internal class Demo3
{

    const int NC = 8;

    internal static void Draw(BoDrawApp bd)
    {
        double sx = 20;
        double sy = 5;
        double dx = 2;
        double dy = 5;

        var colors = typeof(BoDraw.Colors)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.PropertyType == typeof(Color))
            .Select(p => (Name: p.Name, Color: (Color)p.GetValue(null)!))
            .ToArray();

        int cnt = 0;
        double x = 0;
        double y = 0;

        foreach (var c in colors)
        {
            var r = new Rectangle(x, y, x + sx, y + sy);
            r.FillColor = c.Color;

            var t = new Text(c.Name, x, y - 3, 2);

            bd.Add(r, t);

            cnt++;
            if (cnt == NC)
            {
                cnt = 0;
                x = 0;
                y -= sy + dy;
            }
            else
            {
                x += sx + dx;
            }
        }
    }
}