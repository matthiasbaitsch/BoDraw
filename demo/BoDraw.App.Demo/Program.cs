using BoDraw;

BoDrawApp bd = new BoDrawApp();
PolygonDemo.Draw(bd);
bd.SaveImage("test.png", 1200);
// bd.Show();
