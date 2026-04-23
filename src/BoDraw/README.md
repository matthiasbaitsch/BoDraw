# BoDraw

BoDraw is a simple drawing library for .NET.

## Installation

```
dotnet add package BoDraw
```

## Example

```csharp
using BoDraw;

Text text = new Text("BoDraw is a simple drawing library...", -20, -40);
Image image = new Image("assets/hs-bo_logo_en.png", 0, 0, 400);
Rectangle rectangle = new Rectangle(-20, -20, image.Width + 20, image.Height + 20);

rectangle.LineThickness = 3;
rectangle.FillColor = Colors.LightSkyBlue;
rectangle.LineColor = Colors.DarkOrchid;
text.Color = Colors.DarkOliveGreen;

BoDrawApp bd = new BoDrawApp();
bd.Add(text, rectangle, image);
bd.Show();
```

## Shapes

| Class         | Description                               |
|---------------|-------------------------------------------|
| `Line`        | Line between two points                   |
| `Polyline`    | Polyline through multiple points          |
| `Polygon`     | Filled polygon                            |
| `Rectangle`   | Rectangle                                 |
| `Circle`      | Circle                                    |
| `Ellipse`     | Ellipse                                   |
| `Text`        | Text at a position                        |
| `Image`       | Raster image loaded from a file           |

## Coordinate system

The Y-axis points upward (mathematical convention).
