using BoDraw;

BoDrawApp bd = new BoDrawApp();
GroupDemo.Draw(bd);
// bd.Show();
bd.SaveImage("test.png", 1200);
