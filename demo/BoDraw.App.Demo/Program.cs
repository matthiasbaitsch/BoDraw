using BoDraw;

BoDrawApp bd = new BoDrawApp();
PolylineDemo.Draw(bd);
bd.SaveImage("test.png", 1200);
// bd.Show();
