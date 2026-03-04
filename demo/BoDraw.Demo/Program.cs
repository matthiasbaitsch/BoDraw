using bodraw;


// pl.Color = "Red";
// pl.Linewidth = 1.5;

BoDraw bd = new BoDraw();
Polyline pl = new Polyline();
pl.AddPoint(0, 0);
pl.AddPoint(200, 0);
pl.AddPoint(0, 200);
pl.AddPoint(200, 200);
pl.AddPoint(0, 0);
pl.AddPoint(100, -100);
pl.AddPoint(200, 0);
pl.AddPoint(200, 200);
bd.Add(pl);
bd.Show();
