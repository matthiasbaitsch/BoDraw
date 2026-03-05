using bodraw;

BoDraw bd = new BoDraw();

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
bd.Add(l1);

Polyline l2 = new Polyline();
l2.AddPoint(-10, 210);
l2.AddPoint(210, 210);
bd.Add(l2);

bd.Show();
