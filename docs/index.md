# BoDraw

BoDraw is a simple drawing library for .NET built on top of [Avalonia](https://avaloniaui.net/).
It is designed for interactive use in scripts, Jupyter notebooks, and desktop applications.

## Packages

| Package | Description |
|---------|-------------|
| `BoDraw` | Core library — shapes and drawing logic |
| `BoDraw.App` | Avalonia desktop wrapper — opens a window via `bd.Show()` |
| `BoDraw.Interactive` | Jupyter/.NET Interactive wrapper — renders to PNG for notebook display |

## Quick start

```csharp
using BoDraw;

var bd = new BoDrawApp();
bd.Add(
    new Rectangle(0, 0, 4, 3) { FillColor = Colors.SkyBlue },
    new Ellipse(2, 1.5, 1, 1) { FillColor = Colors.Tomato },
    new Line(0, 0, 4, 3) { Color = Colors.DarkSlateGray }
);
bd.Show();
```

## Shape hierarchy

```
Shape (abstract)
├── LineLikeShape (abstract) — Pen, Color, Thickness
│   ├── Line
│   └── Polyline
├── AreaLikeShape (abstract) — Brush, Pen, FillColor, LineColor, LineThickness
│   ├── Rectangle
│   └── Ellipse
├── Image
└── Text
```

## Coordinate system

BoDraw uses **mathematical convention**: the Y-axis points upward.
`Drawing` automatically fits and centers all shapes inside the render target.
