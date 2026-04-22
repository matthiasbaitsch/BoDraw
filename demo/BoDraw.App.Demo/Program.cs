using BoDraw;

Image image = new Image("assets/hs-bo_logo_en.png", 0, 0, 400);
Text text = new Text("BoDraw ist ein einfaches Zeichenpaket...", -20, -40);
Rectangle rectangle = new Rectangle(-20, -20, image.Width + 20, image.Height + 20);

rectangle.LineThickness = 3;
rectangle.FillColor = Colors.LightSkyBlue;
rectangle.LineColor = Colors.DarkOrchid;
text.Color = Colors.DarkOliveGreen;

BoDrawApp bd = new BoDrawApp();
bd.Add(text, rectangle, image);
bd.Show();
