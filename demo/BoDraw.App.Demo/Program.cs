using BoDraw;

BoDrawApp bd = new BoDrawApp();
ClipDemo.Draw(bd);
bd.SaveImage("test.png", 1200);
// bd.Show();
