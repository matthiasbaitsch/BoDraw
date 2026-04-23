# BoDraw

BoDraw is a simple drawing library for .NET built on top of [Avalonia](https://avaloniaui.net/).

## Packages

| Package | Description |
|---------|-------------|
| `BoDraw` | Core library — shapes and drawing logic |
| `BoDraw.App` | Avalonia desktop wrapper — opens a window via `bd.Show()` |

## Quick start

```csharp
using BoDraw;

Rectangle r1 = new Rectangle(0, 0, 12, 8);
r1.FillColor = Colors.HotPink;

Rectangle r2 = new Rectangle(0, 4, 12, 12);
r2.FillColor = Colors.Orange;
r2.FillOpacity = 0.7;

Circle c = new Circle(12, 6, 6);
c.FillColor = Colors.LightBlue;
c.FillOpacity = 0.3;

var bd = new BoDrawApp();
bd.Add(r1, r2, c);
bd.Show();
```

![](images/demo.png)

## Shape hierarchy

```raw
Shape (abstract) - Move(dx, dy), Copy(dx, dy), Scale(a)
├── LineLikeShape (abstract) — Color, Thickness
│   ├── Line
│   └── Polyline
├── AreaLikeShape (abstract) — FillColor, FillOpacity, LineColor, LineThickness
│   ├── Rectangle
│   ├── Polygon
│   ├── Ellipse
│   └── Circle
├── Image
└── Text
```

## Coordinate system

BoDraw uses **mathematical convention**: the Y-axis points upward. `Drawing` automatically fits and centers all shapes inside the render target.
