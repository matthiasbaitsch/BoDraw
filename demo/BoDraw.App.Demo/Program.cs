using bodraw;

Polyline l1 = new Polyline();
l1.AddPoint(200, 200);
l1.AddPoint(0, 0);
l1.AddPoint(100, -100);
l1.AddPoint(200, 0);
l1.AddPoint(0, 0);
l1.AddPoint(0, 200);
l1.AddPoint(200, 200);
l1.AddPoint(200, 0);
l1.AddPoint(0, 200);
l1.Thickness = 2.5;
l1.Color = Colors.HotPink;

Polyline l2 = new Polyline();
l2.AddPoint(-10, 210);
l2.AddPoint(210, 210);

BoDrawApp bd = new BoDrawApp();
bd.Add(l1, l2);
bd.Show();
