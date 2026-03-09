using bodraw;

Polyline l1 = new Polyline();
l1.AddPoint(200, 0);
l1.AddPoint(0, 200);
l1.AddPoint(100, 300);
l1.AddPoint(200, 200);
l1.AddPoint(0, 200);
l1.AddPoint(0, 0);
l1.AddPoint(200, 0);
l1.AddPoint(200, 200);
l1.AddPoint(0, 0);
l1.Thickness = 2.5;
l1.Color = Colors.HotPink;

Polyline l2 = new Polyline();
l2.AddPoint(-40, -10);
l2.AddPoint(240, -10);
l2.AddPoint(240, 310);
l2.AddPoint(-40, 310);
l2.AddPoint(-40, -10);

BoDrawApp bd = new BoDrawApp();
bd.Add(l1, l2);
bd.Show();
