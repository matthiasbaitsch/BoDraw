using bodraw;

Rectangle r1 = new Rectangle(-60, -30, 260, 330);
r1.LineThickness = 6;
r1.LineColor = Colors.SteelBlue;

Rectangle r2 = new Rectangle(-80, -20, -20, 80);
r2.LineThickness = 2;
r2.LineColor = Colors.PaleGreen;
r2.FillColor = null;

Rectangle r3 = new Rectangle(-80, 85, -20, 185);
r3.LineColor = null;
r3.FillColor = Colors.Orange;

Line l1 = new Line(-50, -20, -50, 320);
l1.Thickness = 4;
l1.Color = Colors.Tomato;

Line l2 = new Line(250, -20, 250, 320);
l2.Thickness = 4;
l2.Color = Colors.Tomato;

Polyline pl1 = new Polyline();
pl1.AddPoint(200, 0);
pl1.AddPoint(0, 200);
pl1.AddPoint(100, 300);
pl1.AddPoint(200, 200);
pl1.AddPoint(0, 200);
pl1.AddPoint(0, 0);
pl1.AddPoint(200, 0);
pl1.AddPoint(200, 200);
pl1.AddPoint(0, 0);
pl1.Thickness = 2.5;
pl1.Color = Colors.HotPink;

Polyline pl2 = new Polyline();
pl2.AddPoint(-40, -10);
pl2.AddPoint(240, -10);
pl2.AddPoint(240, 310);
pl2.AddPoint(-40, 310);
pl2.AddPoint(-40, -10);

Ellipse e1 = new Ellipse(100, 300, 10, 5);
e1.FillColor = Colors.BlanchedAlmond;

BoDrawApp bd = new BoDrawApp();
bd.Add(r1, r2, r3, l1, l2, pl1, pl2, e1);
bd.Show();
