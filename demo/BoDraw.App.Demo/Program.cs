using BoDraw;

BoDrawApp bd = new BoDrawApp();
ImageWriteDemo.Draw(bd);
bd.SaveImage("test.png", 1200);
// bd.Show();
